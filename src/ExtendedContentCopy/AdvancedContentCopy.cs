using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Transfer;
using EPiServer.DataAccess;
using EPiServer.Enterprise;
using EPiServer.Logging.Compatibility;
using EPiServer.Framework.Localization;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace ExtendedContentCopy
{
    public interface IAdvancedContentCopy
    {
        ContentReference CopyContent(PasteMode pasteMode, ContentReference contentLink,
            ContentReference destinationLink, AccessLevel requiredSourceAccess);
    }

    [ServiceConfiguration(typeof(IAdvancedContentCopy))]
    public class AdvancedContentCopy: IAdvancedContentCopy
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(AdvancedContentCopy));
        private readonly IContentRepository _contentRepository;
        private readonly ContentOptions _contentOptions;
        private readonly ServiceAccessor<IDataImporter> _dataImporterAccessor;
        private readonly ServiceAccessor<IDataExporter> _dataExporterAccessor;

        public AdvancedContentCopy(IContentRepository contentRepository, ContentOptions contentOptions,
            ServiceAccessor<IDataImporter> dataImporterAccessor, ServiceAccessor<IDataExporter> dataExporterAccessor)
        {
            _contentRepository = contentRepository;
            _contentOptions = contentOptions;
            _dataImporterAccessor = dataImporterAccessor;
            _dataExporterAccessor = dataExporterAccessor;
        }


        public ContentReference CopyContent(PasteMode pasteMode, ContentReference contentLink, ContentReference destinationLink, AccessLevel requiredSourceAccess)
        {
            var pageCount = _contentRepository.GetDescendents(contentLink).Count() + 1;

            var recursionLevel = pasteMode.CopyDescendants == false ? 0 : int.MaxValue;

            var exportSources = new List<ExportSource> { new ExportSource(contentLink, recursionLevel) };

            var languageCode = Thread.CurrentThread.CurrentUICulture.Name;

            var content = _contentRepository.Get<PageData>(contentLink);

            var result = ContentReference.EmptyReference;

            var useFile = pageCount > _contentOptions.InMemoryCopyThreshold;
            var filePath = useFile ? Path.GetTempFileName() : null;
            try
            {
                using (var stream = CreateStream(useFile, filePath))
                {
                    using (var dataExporter = _dataExporterAccessor())
                    {
                        var exportOptions = new ExportOptions
                        {
                            TransferType = TypeOfTransfer.Copying,
                            RequiredSourceAccess = requiredSourceAccess,
                            AutoCloseStream = false,
                            ExcludeFiles = false
                        };
                        var options = exportOptions;
                        dataExporter.Export(stream, exportSources, options);
                        if (dataExporter.Status.Log.Errors.Count > 0)
                        {
                            Rollback(result, contentLink, destinationLink, languageCode, dataExporter.Status.Log);
                        }
                    }
                    stream.Seek(0L, SeekOrigin.Begin);
                    var dataImporter = _dataImporterAccessor();
                    var importOptions = new ImportOptions
                    {
                        TransferType = TypeOfTransfer.Copying,
                        EnsureContentNameUniqueness = true,
                        SaveAction = (SaveAction)((pasteMode.PublishOnDestination.Value ? 3 : 6) | 512),
                    };
                    if (pasteMode.CopyAllLanguageBranches == false)
                    {
                        importOptions.SelectedLanguage = content.MasterLanguage;
                    }
                    dataImporter.Import(stream, destinationLink, importOptions);
                    result = dataImporter.Status.ImportedRoot;
                    if (dataImporter.Status.Log.Errors.Count <= 0)
                    {
                        return result;
                    }
                    Rollback(result, contentLink, destinationLink, languageCode, dataImporter.Status.Log);
                }
            }
            finally
            {
                if (filePath != null && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }

            return result;
        }

        private void Rollback(ContentReference newLink, ContentReference originalContentLink, ContentReference destinationLink, string languageCode, ITransferLogger transferLogger)
        {
            try
            {
                if (!ContentReference.IsNullOrEmpty(newLink))
                {
                    _contentRepository.Delete(newLink, true, AccessLevel.NoAccess);
                }
            }
            catch (Exception ex)
            {
                transferLogger.Error($"Failed to rollback creation of '{newLink}'", ex, Array.Empty<object>());
            }

            if (_log.IsErrorEnabled)
            {
                _log.ErrorFormat("Failed to copy pages with root '{0}' to '{1}'", originalContentLink.ToString(), destinationLink.ToString());
            }

            var stringByCulture = LocalizationService.Current.GetStringByCulture("/copy/backgroundreport/failed", "Failed to copy content '{0}' to '{1}'", CultureInfo.GetCultureInfo(languageCode));
            var content1 = _contentRepository.Get<IContent>(originalContentLink);
            var str1 = content1?.Name;
            var content2 = _contentRepository.Get<IContent>(destinationLink);
            var str2 = content2?.Name;
            throw new EPiServerException($"{string.Format(stringByCulture, str1, str2)}:{string.Join(",", transferLogger.Errors.Cast<string>().ToArray())}");
        }

        private static Stream CreateStream(bool useFile, string filePath)
        {
            if (useFile)
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            }
            return new MemoryStream();
        }
    }
}