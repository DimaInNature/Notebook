namespace Services.Users;

public class UserRepositoryService : IUserRepositoryService
{
    private readonly IUserRepository _repository;

    public UserRepositoryService(IUserRepository repository)
    {
        _repository = repository;
    }

    public void AddUser(User userItem) =>
        _repository.AddUserAsync(userItem);

    public void DeleteUser(User userItem) =>
        _repository.DeleteUser(userItem);

    public IEnumerable<User> GetAll() =>
        _repository.GetAll();

    public User GetUserById(int id) =>
        _repository.GetUserById(id);
}
