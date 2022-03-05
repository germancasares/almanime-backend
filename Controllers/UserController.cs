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
    private readonly IRoleService _roleService;

    public UserController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var users = _userService.Get();

        return Ok(users.Select(user => new {
            user.Name
        }));
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var auth0ID = User.GetAuth0ID();

        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));

        var user = _userService.GetByAuth0ID(auth0ID);

        if (user == null) return NotFound();

        return Ok(new {
            user.Name,
            Permissions = _roleService.GetByUser(user),
        });
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post(UserDTO userDTO)
    {
        var auth0ID = User.GetAuth0ID();

        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));
        if (userDTO.Name == null) throw new ArgumentNullException(nameof(userDTO));

        _userService.Create(auth0ID, userDTO.Name);

        return Ok();
    }

    [HttpPut]
    [Authorize]
    public IActionResult Patch(UserDTO userDTO)
    {
        var auth0ID = User.GetAuth0ID();

        if (auth0ID == null) throw new ArgumentNullException(nameof(auth0ID));
        if (userDTO.Name == null) throw new ArgumentNullException(nameof(userDTO));

        _userService.Update(auth0ID, userDTO.Name);

        return Ok();
    }
}
