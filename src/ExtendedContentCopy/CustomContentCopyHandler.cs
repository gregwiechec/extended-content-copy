using EPiServer.Core;
using EPiServer.Core.Transfer;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace ExtendedContentCopy
{
    /// <summary>
    /// Custom implementation of <see cref="IContentCopyHandler"/>
    /// that allows to copy unpublished pages, one language branch and no descendants
    /// </summary>
    public class CustomContentCopyHandler: IContentCopyHandler
    {
        private readonly IContentCopyHandler _contentCopyHandler;
        private readonly ExtendedContentCopyOptions _options;
        private readonly IUserPasteModeLoader _userPasteModeLoader;
        private readonly IAdvancedContentCopy _advancedContentCopy;

        public CustomContentCopyHandler(IContentCopyHandler contentCopyHandler,
            ExtendedContentCopyOptions options,
            IUserPasteModeLoader userPasteModeLoader,
            IAdvancedContentCopy advancedContentCopy)
        {
            _contentCopyHandler = contentCopyHandler;
            _options = options;
            _userPasteModeLoader = userPasteModeLoader;
            _advancedContentCopy = advancedContentCopy;
        }

        public CustomContentCopyHandler(IContentCopyHandler contentCopyHandler, IServiceLocator serviceLocator) :
            this(contentCopyHandler,
                serviceLocator.GetInstance<ExtendedContentCopyOptions>(),
                serviceLocator.GetInstance<IUserPasteModeLoader>(),
                serviceLocator.GetInstance<IAdvancedContentCopy>()
            )
        {
        }

        public ContentReference Copy(ContentReference contentLink, ContentReference destinationLink, AccessLevel requiredSourceAccess,
            bool publishOnDestination)
        {
            // when mode is `off` then use default IContentCopyHandler implementation
            if (_options.Mode == ExtendedContentCopyOptions.ExtendedContentCopyMode.Off)
            {
                return _contentCopyHandler.Copy(contentLink, destinationLink, requiredSourceAccess, publishOnDestination);
            }

            // when mode is `auto` then use default settings
            PasteMode pasteMode;
            if (_options.Mode == ExtendedContentCopyOptions.ExtendedContentCopyMode.Auto)
            {
                pasteMode = new PasteMode(_options);
            }
            else // mode is `command`
            {
                pasteMode = _userPasteModeLoader.Load();
                // when paste mode was not sent from client then return default
                if (pasteMode == null)
                {
                    return _contentCopyHandler.Copy(contentLink, destinationLink, requiredSourceAccess, publishOnDestination);
                }
            }

            // merge options with available settings
            pasteMode = PredefinedOptionsMerger.MergeWithAvailableOptions(pasteMode, _options);

            // this one special case can be handled by built-in Copy method
            if (pasteMode.CopyAllLanguageBranches != true &&
                pasteMode.CopyDescendants != true &&
                pasteMode.PublishOnDestination == true)
            {
                return _contentCopyHandler.Copy(contentLink, destinationLink, requiredSourceAccess, true);
            }

            // use advanced content copy to paste content with options
            return _advancedContentCopy.CopyContent(pasteMode, contentLink, destinationLink, requiredSourceAccess);
        }
    }
}