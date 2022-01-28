namespace Services.Abstract.Users;

public interface IUserFacadeService
{
    bool Register(User userItem);
    void DeleteUser(User userItem);
    IEnumerable<User> GetAll();
    User GetUserById(int id);
    bool Authorize(User user);
}
