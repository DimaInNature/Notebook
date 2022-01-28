namespace Services.Abstract.Users;

public interface IUserRepositoryService
{
    void AddUser(User userItem);
    void DeleteUser(User userItem);
    IEnumerable<User> GetAll();
    User GetUserById(int id);
}
