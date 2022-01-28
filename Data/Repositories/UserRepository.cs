namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User userItem)
    {
        await _context.Users.AddAsync(entity: userItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(User userItem)
    {
        _context.Users.Attach(entity: userItem);
        _context.Users.Remove(entity: userItem);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<User> GetAll() => _context.Users;

    public User GetUserById(int id) =>
        _context.Users.FirstOrDefault(user => user.Id == id);
}