using EPiServer.Core.Transfer;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace ExtendedContentCopy
{
    [InitializableModule]
    public class ExtendedContentCopyInitialization: IConfigurableModule
    {
        public void Initialize(InitializationEngine context)
        {
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.ConfigurationComplete += Context_ConfigurationComplete;
        }

        private static void Context_ConfigurationComplete(object sender, ServiceConfigurationEventArgs e)
        {
            e.Services.Intercept<IContentCopyHandler>((locator, defaultContentCopyHandler) =>
                new ExtendedContentCopyHandler(defaultContentCopyHandler, locator));
        }
    }
}