namespace Notebook;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; }
    private ApplicationConfiguratorService _configuratorService;

    public App()
    {
        _configuratorService = new();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<ApplicationConfiguratorService>();

        ConfigureServices(services: serviceCollection);

        ConfigureViewModels(services: serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        ServiceProvider.GetService<ApplicationRunnerService>()?.Run();

        var mainWindow = new LoginView
        {
            DataContext = ServiceProvider.GetService<LoginViewModel>()
        };

        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlite(_configuratorService.DataBaseConnection);
        });

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddTransient<IUserRepositoryService, UserRepositoryService>();
        services.AddTransient<IUserFacadeService, UserFacadeService>();

        services.AddSingleton<INoteRepository, NoteRepository>();
        services.AddSingleton<INoteRepositoryService, NoteRepositoryService>();
        services.AddSingleton<INoteFacadeService, NoteFacadeService>();

        services.AddSingleton<IActiveSessionService, ActiveSessionService>();
        services.AddTransient<ResourcesDictionaryManagerService>();
        services.AddTransient<ApplicationRunnerService>();
        services.AddTransient<IUserAuthorizationService, UserAuthorizationService>();
        services.AddTransient<IUserRegistrationService, UserRegistrationService>();
    }

    private void ConfigureViewModels(IServiceCollection services)
    {
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<NoteViewModel>();
        services.AddTransient<CreateNoteViewModel>();
        services.AddTransient<DeleteNoteViewModel>();
        services.AddTransient<SettingsViewModel>();
    }
}
