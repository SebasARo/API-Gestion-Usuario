
using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

//---------------------------- Creamos los usuarios para la API --------------------------------------------------------------------------
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>
    {
            new User { Id = 1, Name = "John Doe", Password = "password123", Age = 30, Email = "john.doe@example.com" },

            new User { Id = 2, Name = "Sebas Arias", Password = "securepass456", Age = 26, Email = "sebas12@gmail.com" }
    };

    //---------------------------------------- GET: api/User --------------------------------------------------------------
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(users);
    }

//---------------------------- Mostrar solo el usuario por id --------------------------------------------------------------------------

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

//---------------------------- Crear un nuevo usuario --------------------------------------------------------------------------
        [HttpPost]
public ActionResult<User> CreateUser(User user)
{
    try
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        users.Add(user);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    catch (Exception)
    {
        return StatusCode(500, "An unexpected error occurred.");
    }
}

//---------------------------- Actualizar un usuario existente por su ID --------------------------------------------------------------------------

        [HttpPut("{id}")]
public IActionResult UpdateUser(int id, User updatedUser)
{
    try
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        user.Name = updatedUser.Name;
        user.Password = updatedUser.Password;
        user.Age = updatedUser.Age;
        user.Email = updatedUser.Email;

        return Ok(user);
    }
    catch (Exception)
    {
        return StatusCode(500, "An unexpected error occurred.");
    }
}

//---------------------------- Eliminar un usuario por su ID --------------------------------------------------------------------------

        [HttpDelete("{id}")]
         public IActionResult DeleteUser(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        users.Remove(user);
        return Ok($"User with ID: {id} and Name: {user.Name} has been deleted.");
        
    }
    }
}
