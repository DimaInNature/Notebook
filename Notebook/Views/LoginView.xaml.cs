namespace Notebook.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
        DataContext = (Application.Current as App)?
               .ServiceProvider.GetService<LoginViewModel>();
    }

    private LoginViewModel ViewModel => (LoginViewModel)DataContext;

    private void WindowDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void EnterPasswordPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e) =>
        (ViewModel.Password, ViewModel.SecurePassword) =
        (EnterPasswordPasswordBox.Password, EnterPasswordPasswordBox.SecurePassword);

    private void RegisterPasswordPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e) =>
        (ViewModel.RegisterPassword, ViewModel.SecureRegisterPassword) =
        (RegisterPasswordPasswordBox.Password, RegisterPasswordPasswordBox.SecurePassword);
}
