namespace Services.Abstract.Users;

public interface IUserAuthorizationService
{
    bool Authorize(User user);
}
