using Almanime.Models.DTO;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Almanime.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var users = _userService.Get();

        return Ok(users.Select(user => new {
            user.Name
        }));
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post(UserDTO userDTO)
    {
        var auth0ID = User.GetAuth0ID();

        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));
        if (userDTO.Name == null) throw new ArgumentNullException(nameof(userDTO.Name));

        _userService.Create(auth0ID, userDTO.Name);

        return Ok();
    }
}
