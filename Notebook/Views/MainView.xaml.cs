namespace Notebook.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        DataContext = (Application.Current as App)?
                .ServiceProvider.GetService<MainViewModel>();
    }

    private void WindowDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }
}
