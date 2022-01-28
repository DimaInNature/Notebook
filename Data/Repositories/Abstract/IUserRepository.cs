namespace Data.Repositories.Abstract;

public interface IUserRepository
{
    Task AddUserAsync(User userItem);
    Task DeleteUser(User userItem);
    User GetUserById(int id);
    IEnumerable<User> GetAll();
}
