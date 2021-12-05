using System.Collections.Generic;
using EPiServer.Cms.Shell.Internal;
using EPiServer.Framework.Web.Resources;
using EPiServer.Shell;
using EPiServer.Shell.Modules;

namespace ExtendedContentCopy.EditMode
{
    /// <summary>
    /// Extended content copy viewmodel used in edit mode
    /// </summary>
    public class ExtendedContentCopyModuleViewModel : CmsModuleViewModel
    {
        public bool IsCommandAvailable { get; private set; }
        public ExtendedContentCopyOptions.Defaults PasteDefaults { get; set; }
        public ExtendedContentCopyOptions.AllowedActions AllowedPasteActions { get; set; }

        public ExtendedContentCopyModuleViewModel(ShellModule shellModule,
            IClientResourceService clientResourceService,
            IEnumerable<IContentRepositoryDescriptor> contentRepositoryDescriptors,
            ExtendedContentCopyOptions options) :
            base(shellModule, clientResourceService, contentRepositoryDescriptors)
        {
            IsCommandAvailable = options.Mode == ExtendedContentCopyOptions.ExtendedContentCopyMode.Command;
            PasteDefaults = options.PasteDefaults;
            AllowedPasteActions = options.AllowedPasteActions;
        }
    }
}