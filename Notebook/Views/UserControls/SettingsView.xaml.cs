using System.Windows.Controls;

namespace Notebook.Views.UserControls;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        DataContext = (Application.Current as App)?
            .ServiceProvider.GetService<SettingsViewModel>();
    }
}
