namespace Services;

public class ApplicationRunnerService
{
    private readonly ApplicationConfiguratorService _configurator;

    private readonly ResourcesDictionaryManagerService _recourceManager;

    public ApplicationRunnerService(ApplicationConfiguratorService configurator,
        ResourcesDictionaryManagerService recourceManager)
    {
        _configurator = configurator;
        _recourceManager = recourceManager;
    }

    public void Run()
    {
        var theme = _configurator.Theme;
        _recourceManager.SwitchTheme(theme);
    }
}
