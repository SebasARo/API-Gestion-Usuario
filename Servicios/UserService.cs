

namespace UserManagementAPI.Services;

public class UserService : IUserService
{
    private static List<User> users = new()
    {
        new User
        {
            Id = 1,
            Name = "John Doe",
            Age = 30,
            Email = "john.doe@example.com"
        },

        new User
        {
            Id = 2,
            Name = "Sebas Arias",
            Age = 26,
            Email = "sebas12@gmail.com"
        }
    };

    public List<User> GetUsers()
    {
        return users;
    }

    public User? GetUser(int id)
    {
        return users.FirstOrDefault(u => u.Id == id);
    }

    public User? CreateUser(User user)
    {
        // Verificar si ya existe un usuario con el mismo Id
    if (users.Any(u => u.Id == user.Id))
    {
        return null;
    }

    users.Add(user);
    return user;
    }

    public bool UpdateUser(int id, User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return false;

        user.Name = updatedUser.Name;
        user.Age = updatedUser.Age;
        user.Email = updatedUser.Email;

        return true;
    }

    public bool DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return false;

        users.Remove(user);

        return true;
    }
}