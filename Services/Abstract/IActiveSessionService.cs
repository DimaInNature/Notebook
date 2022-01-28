namespace Services.Abstract;

public interface IActiveSessionService
{
    void StartSession(User user);
    void EndSession();
    User GetUser();
    bool GetStatus();
}
