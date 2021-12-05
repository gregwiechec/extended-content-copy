Plugin works without providing configuration.

The default behaviour can be changed using Options:

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
                options.Mode = ExtendedContentCopyOptions.ExtendedContentCopyMode.Auto;
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
