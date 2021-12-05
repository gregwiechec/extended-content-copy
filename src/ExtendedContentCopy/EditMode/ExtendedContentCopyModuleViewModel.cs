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
        public ExtendedContentCopyOptions.CommandOptions Command { get; private set; }

        public ExtendedContentCopyModuleViewModel(ShellModule shellModule,
            IClientResourceService clientResourceService,
            IEnumerable<IContentRepositoryDescriptor> contentRepositoryDescriptors,
            ExtendedContentCopyOptions options) :
            base(shellModule, clientResourceService, contentRepositoryDescriptors)
        {
            Command = options.Command;
        }
    }
}