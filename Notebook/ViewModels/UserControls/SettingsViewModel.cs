namespace Notebook.ViewModels.UserControls;

public class SettingsViewModel : BaseViewModel
{
    private readonly ResourcesDictionaryManagerService _recourceManager;
    private readonly ApplicationConfiguratorService _configurator;

    public SettingsViewModel(ResourcesDictionaryManagerService recourceManager,
        ApplicationConfiguratorService configurator)
    {
        _recourceManager = recourceManager;
        _configurator = configurator;

        SelectedIndex = (int)_configurator.Theme;

        InitializeCommands();
    }

    #region Data

    public List<string> ThemeItems
    {
        get => _themeItems;
        set
        {
            _themeItems = value;
            OnPropertyChanged(nameof(ThemeItems));
        }
    }

    private List<string> _themeItems = new()
    {
        ThemeType.Classic.ToString(),
        ThemeType.Dark.ToString(),
    };

    public int SelectedIndex
    {
        get => _selectedIndex;
        set
        {
            _selectedIndex = value;
            OnPropertyChanged(nameof(SelectedIndex));
        }
    }

    private int _selectedIndex;

    #endregion

    #region Commands

    public ICommand ThemeChangedCommand { get; private set; }

    #endregion

    #region Command logic

    #region Execute

    private void ExecuteThemeChangedCommand(object obj)
    {
        _configurator.Theme = (ThemeType)SelectedIndex;
        _recourceManager.SwitchTheme(_configurator.Theme);
    }

    #endregion

    #endregion

    #region Other logic

    private void InitializeCommands()
    {
        ThemeChangedCommand = new RelayCommand(executeAction: ExecuteThemeChangedCommand);
    }

    #endregion
}
