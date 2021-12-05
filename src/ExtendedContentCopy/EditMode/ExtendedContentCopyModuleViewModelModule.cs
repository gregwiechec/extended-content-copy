﻿/*using System.Collections.Generic;
using ContentChildrenGrouping.Core;
using EPiServer.Framework.Web.Resources;
using EPiServer.ServiceLocation;
using EPiServer.Shell;
using EPiServer.Shell.Modules;

namespace ContentChildrenGrouping.VirtualContainers.EditMode
{
    public class ContentChildrenGroupingModuleViewModelModule : ShellModule
    {
        private readonly IEnumerable<IContentRepositoryDescriptor> _contentRepositoryDescriptors;
        private readonly IEnumerable<IContentChildrenGroupsLoader> _contentChildrenGroupsLoaders;
        private readonly VirtualContainersOptions _virtualContainersOptions;

        public ContentChildrenGroupingModuleViewModelModule(string name, string routeBasePath, string resourceBasePath) :
            base(name, routeBasePath, resourceBasePath)
        {
            _virtualContainersOptions = ServiceLocator.Current.GetInstance<VirtualContainersOptions>();
            _contentRepositoryDescriptors = ServiceLocator.Current.GetAllInstances<IContentRepositoryDescriptor>();
            _contentChildrenGroupsLoaders = ServiceLocator.Current.GetAllInstances<IContentChildrenGroupsLoader>();
        }

        public override ModuleViewModel CreateViewModel(ModuleTable moduleTable,
            IClientResourceService clientResourceService)
        {
            return new ContentChildrenGroupingModuleViewModel(this, clientResourceService,
                _contentRepositoryDescriptors, _contentChildrenGroupsLoaders, _virtualContainersOptions);
        }
    }
}*/