
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

//---------------------------- Creamos los usuarios para la API --------------------------------------------------------------------------
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
           {
              _userService = userService;
           }

    //---------------------------------------- GET: api/User --------------------------------------------------------------
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return Ok(_userService.GetUsers());
    }

//---------------------------- Mostrar solo el usuario por id --------------------------------------------------------------------------

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userService.GetUser(id);

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

       var createdUser = _userService.CreateUser(user);
       if (createdUser == null)
        {
            return Conflict($"Ya existe un usuario con el Id {user.Id}.");
        }

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }
    catch (Exception)
    {
        return StatusCode(500, "Ocurrió un error inesperado.");
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

        var update = _userService.UpdateUser(id, updatedUser);

        if (!update)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }
    catch (Exception)
    {
        return StatusCode(500, "Ocurrió un error inesperado.");
    }
}

//---------------------------- Eliminar un usuario por su ID --------------------------------------------------------------------------

        [HttpDelete("{id}")]
         public IActionResult DeleteUser(int id)
    {
        var delete = _userService.DeleteUser(id);

        if (!delete)
        {
            return NotFound();
        }

        return Ok($"El Usuario con ID: {id} ha sido eliminado.");
    }
    }
}
