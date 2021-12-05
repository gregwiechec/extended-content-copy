using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using ExtendedContentCopy;

namespace Alloy.Sample.Business.Initialization
{
    [InitializableModule]
    public class ExtendedContentCopyInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.ConfigurationComplete += (o, e) =>
                {
                    // override default behaviour and allow to copy descendants
                    context.Services.Configure<ExtendedContentCopyOptions>(options =>
                        {
                            options.AllowedPasteActions.CopyDescendants = true;
                        });
                };
        }

        public void Initialize(InitializationEngine context)
        {
          
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}
