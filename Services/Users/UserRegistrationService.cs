namespace Services.Users;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUserRepositoryService _repository;

    public UserRegistrationService(IUserRepositoryService repositoryService)
    {
        _repository = repositoryService;
    }

    public bool Register(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        int usersByLoginCount = _repository.GetAll()
            .Where(u => u.Login == user.Login)
            .Count();

        if (usersByLoginCount < 1)
        {
            int usersByLoginAndPasswordCount = _repository.GetAll()
                .Where(u => u.Login == user.Login && u.Password == user.Password)
                .Count();

            if (usersByLoginAndPasswordCount < 1)
            {
                _repository.AddUser(user);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
