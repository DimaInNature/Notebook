namespace Services;

public class ActiveSessionService : IActiveSessionService
{
    private readonly IUserRepositoryService _userRepository;

    public ActiveSessionService(IUserRepositoryService userRepositoryService)
    {
        _userRepository = userRepositoryService;
    }

    private User _activeUser;

    private bool _isActive = false;

    public bool GetStatus() => _isActive;

    public void StartSession(User activeUser)
    {
        if (_isActive is false)
        {
            _activeUser = string.IsNullOrWhiteSpace(activeUser.Login) ||
                string.IsNullOrWhiteSpace(activeUser.Password)
                ? throw new ArgumentException("Invalid user data")
                : _userRepository.GetAll().FirstOrDefault(
                    user => user.Login == activeUser.Login &&
                    user.Password == activeUser.Password)
                ?? throw new ArgumentNullException("There is no user with such data");

            _isActive = true;
        }
    }

    public User GetUser() => GetStatus()
        ? _activeUser
        : throw new InvalidOperationException("The session is not active.");

    public void EndSession()
    {
        if (_isActive)
        {
            _activeUser = null;

            _isActive = false;
        }
    }
}
