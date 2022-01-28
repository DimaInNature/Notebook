using Models.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Users")]
public class User : BaseModel
{
    public string Login { get; set; }
    public string Password { get; set; }

    public User(string login, string password) =>
        (Login, Password) = (login, password);

    public User() { }
}