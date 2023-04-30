using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
      user.Name,
      Fansubs = user.Memberships.Select(membership => new { membership.FansubRole.Fansub.Acronym, membership.FansubRole.Fansub.Name }),
    }));
  }

  [HttpGet("me")]
  [Authorize]
  public IActionResult Me()
  {
    var auth0ID = User.GetAuth0ID();

    var user = _userService.GetByAuth0ID(auth0ID);

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

    if (userDTO.Name == null) throw new AlmNullException(nameof(userDTO.Name));

    _userService.Create(auth0ID, userDTO.Name);
    Log.Information("User {Auth0ID} has been created with Name {Name}", auth0ID, userDTO.Name);

    return Ok();
  }

  [HttpPut]
  [Authorize]
  public IActionResult Patch(UserDTO userDTO)
  {
    var auth0ID = User.GetAuth0ID();

    if (userDTO.Name == null) throw new AlmNullException(nameof(userDTO.Name));

    _userService.Update(auth0ID, userDTO.Name);
    Log.Information("User {Auth0ID} has changed Name to {Name}", auth0ID, userDTO.Name);

    return Ok();
  }
}
