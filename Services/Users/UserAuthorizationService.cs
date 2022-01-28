namespace Services.Users;

public class UserAuthorizationService : IUserAuthorizationService
{
    private readonly IUserRepositoryService _repository;

    public UserAuthorizationService(IUserRepositoryService repository)
    {
        _repository = repository;
    }

    public bool Authorize(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        var existUser = _repository.GetAll()
            .FirstOrDefault(u => u.Login == user.Login &&
            user.Password == user.Password);

        return existUser is not null;
    }
}
