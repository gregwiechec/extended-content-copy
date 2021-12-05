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
        public bool CommandEnabled { get; set; }

        public bool CopyDescendants { get; set; }

        public bool CopyAllLanguageBranches { get; set; }

        public bool PublishOnDestination { get; set; }

        public ExtendedContentCopyModuleViewModel(ShellModule shellModule,
            IClientResourceService clientResourceService,
            IEnumerable<IContentRepositoryDescriptor> contentRepositoryDescriptors,
            ExtendedContentCopyOptions options) :
            base(shellModule, clientResourceService, contentRepositoryDescriptors)
        {
            CommandEnabled = options.Command.Enabled;
            PublishOnDestination = options.Command.PublishOnDestination;
            CopyAllLanguageBranches = options.Command.CopyAllLanguageBranches;
            CopyDescendants = options.Command.CopyDescendants;
        }
    }
}