
public interface IUserService
{
    List<User> GetUsers();

    User? GetUser(int id);

    User CreateUser(User user);

    bool UpdateUser(int id, User user);

    bool DeleteUser(int id);
}