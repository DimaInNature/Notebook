namespace Services.Users;

public class UserFacadeService : IUserFacadeService, IUserAuthorizationService, IUserRegistrationService
{
    private readonly IUserRepositoryService _repository;
    private readonly IUserAuthorizationService _authorizationService;
    private readonly IUserRegistrationService _registrationService;
    private readonly IActiveSessionService _activeSessionService;

    public UserFacadeService(IUserRepositoryService repository,
        IUserAuthorizationService authorizationService, IActiveSessionService activeSession,
        IUserRegistrationService registrationService)
    {
        _repository = repository;
        _authorizationService = authorizationService;
        _activeSessionService = activeSession;
        _registrationService = registrationService;
    }

    public bool Register(User userItem)
    {
        bool registerIsSuccessfull =
            _registrationService.Register(user: userItem);

        if (registerIsSuccessfull)
            _activeSessionService.StartSession(user: userItem);

        return registerIsSuccessfull;
    }

    public void DeleteUser(User userItem) =>
        _repository.DeleteUser(userItem);

    public IEnumerable<User> GetAll() =>
        _repository.GetAll();

    public User GetUserById(int id) =>
        _repository.GetUserById(id);

    public bool Authorize(User user)
    {
        bool authIsSuccessfull = _authorizationService.Authorize(user);

        if (authIsSuccessfull)
            _activeSessionService.StartSession(user: user);

        return authIsSuccessfull;
    }
}
