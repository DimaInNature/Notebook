using System.Runtime.InteropServices;
using System.Security;

namespace Notebook.ViewModels;

public sealed class LoginViewModel : BaseViewModel
{
    private readonly IUserFacadeService _userFacadeService;

    public LoginViewModel(IUserFacadeService userFacadeService)
    {
        _userFacadeService = userFacadeService;

        SetViewCondition();
        InitializeCommands();
    }

    #region Properties

    #region Login

    public string EnterUserLogin
    {
        get => _enterUserLogin;
        set
        {
            _enterUserLogin = string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value;
            OnPropertyChanged(nameof(EnterUserLogin));
        }
    }

    private string _enterUserLogin;

    public SecureString SecurePassword
    {
        get => _securePassword;
        set
        {
            _securePassword = value;
            OnPropertyChanged(nameof(SecurePassword));
            OnPropertyChanged(nameof(Password));
        }
    }

    private SecureString _securePassword;

    public unsafe string Password
    {
        [SecurityCritical]
        get => SecurePassword is null
            ? string.Empty
            : new string(value: (char*)(void*)Marshal.SecureStringToBSTR(s: SecurePassword));
        [SecurityCritical]
        set
        {
            if (value is null)
                value = string.Empty;

            using (SecureString secureString = new SecureString())
            {
                for (int i = 0; i < value.Length; i++)
                {
                    secureString.AppendChar(c: value[i]);
                }
            }
            _password = value;
            IsPasswordWatermarkVisible = string.IsNullOrEmpty(_password);
        }
    }

    private string _password;

    #endregion

    #region Registration

    public string CreateUserLogin
    {
        get => _createUserLogin;
        set
        {
            _createUserLogin = string.IsNullOrWhiteSpace(value)
                ? string.Empty
                : value;
            OnPropertyChanged(nameof(CreateUserLogin));
        }
    }

    private string _createUserLogin;

    public SecureString SecureRegisterPassword
    {
        get => _secureRegisterPassword;
        set
        {
            _secureRegisterPassword = value;
            OnPropertyChanged(nameof(SecureRegisterPassword));
        }
    }

    private SecureString _secureRegisterPassword;

    public string RegisterPassword
    {
        get => _registerPassword;
        set
        {
            _registerPassword = value;
            IsRegisterPasswordWatermarkVisible = string.IsNullOrEmpty(_registerPassword);
        }
    }

    private string _registerPassword;

    #endregion

    #region Commands

    public ICommand LoginCommand { get; private set; }

    public ICommand RegistrationCommand { get; private set; }

    public ICommand CloseApplicationCommand { get; private set; }

    public ICommand RecoverCommand { get; private set; }

    #endregion

    #region View

    public bool IsPasswordWatermarkVisible
    {
        get => _isPasswordWatermarkVisible;
        set
        {
            if (value.Equals(_isPasswordWatermarkVisible)) return;
            _isPasswordWatermarkVisible = value;
            OnPropertyChanged(nameof(IsPasswordWatermarkVisible));
        }
    }

    private bool _isPasswordWatermarkVisible;

    public bool IsRegisterPasswordWatermarkVisible
    {
        get => _isRegisterPasswordWatermarkVisible;
        set
        {
            if (value.Equals(_isRegisterPasswordWatermarkVisible)) return;
            _isRegisterPasswordWatermarkVisible = value;
            OnPropertyChanged(nameof(IsRegisterPasswordWatermarkVisible));
        }
    }

    private bool _isRegisterPasswordWatermarkVisible;

    #endregion

    #endregion

    #region Commands

    private bool CanExecuteLogin(object obj) =>
        StringHelper.StrIsNotNullOrWhiteSpace(EnterUserLogin, Password);

    private void ExecuteLogin(object obj)
    {
        User authorizedUser = new(login: EnterUserLogin,
            password: Password);

        bool authIsSuccessfull = _userFacadeService
            .Authorize(user: authorizedUser);

        if (authIsSuccessfull)
        {
            new MainView().Show();

            (obj as LoginView)?.Close();
        }
        else
            MessageBox.Show(messageBoxText: "Пользователя с таким логином не существует",
                        caption: "Ошибка", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
    }

    private bool CanExecuteRegistration(object obj) =>
        StringHelper.StrIsNotNullOrWhiteSpace(CreateUserLogin, RegisterPassword);

    private void ExecuteRegistration(object obj)
    {
        User registerUser = new(login: CreateUserLogin,
            password: RegisterPassword);

        bool registerIsSuccessfull =
            _userFacadeService.Register(userItem: registerUser);

        if (registerIsSuccessfull)
        {
            new MainView().Show();

            (obj as LoginView)?.Close();
        }
        else
            MessageBox.Show(messageBoxText: "Пользователь с таким логином уже существует",
                caption: "Ошибка", button: MessageBoxButton.OK, icon: MessageBoxImage.Error);
    }

    private static void CloseApplication(object obj) =>
        Application.Current.Shutdown();

    #endregion

    #region Methods

    private void SetViewCondition()
    {
        IsPasswordWatermarkVisible = IsRegisterPasswordWatermarkVisible = true;
    }

    private void InitializeCommands()
    {
        LoginCommand = new RelayCommand(executeAction: ExecuteLogin,
            canExecuteFunc: CanExecuteLogin);

        RegistrationCommand = new RelayCommand(executeAction: ExecuteRegistration,
            canExecuteFunc: CanExecuteRegistration);

        CloseApplicationCommand = new RelayCommand(executeAction: CloseApplication);
    }

    #endregion
}