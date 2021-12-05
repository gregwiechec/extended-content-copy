using System.Collections.Generic;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Shell.Modules;

namespace ExtendedContentCopy.EditMode
{
    /// <summary>
    /// Responsible for returning viewmodel for extended content copy module
    /// </summary>
    public class ExtendedContentCopyModuleViewModelModule : ShellModule
    {
        private readonly IEnumerable<IContentRepositoryDescriptor> _contentRepositoryDescriptors;
        private readonly ExtendedContentCopyOptions _extendedContentCopyOptions;

        public ExtendedContentCopyModuleViewModelModule(string name, string routeBasePath, string resourceBasePath) :
            base(name, routeBasePath, resourceBasePath)
        {
            _extendedContentCopyOptions = ServiceLocator.Current.GetInstance<ExtendedContentCopyOptions>();
            _contentRepositoryDescriptors = ServiceLocator.Current.GetAllInstances<IContentRepositoryDescriptor>();
        }

        public override ModuleViewModel CreateViewModel(ModuleTable moduleTable,
            IClientResourceService clientResourceService)
        {
            return new ExtendedContentCopyModuleViewModel(this, clientResourceService, _contentRepositoryDescriptors, _extendedContentCopyOptions);
        }
    }
}