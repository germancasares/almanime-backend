using Almanime.Models;
using Almanime.Models.DTO;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Almanime.Controllers.v1;

[Route("v1/[controller]")]
[ApiController]
public class UserController(IUserService userService, IRoleService roleService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IRoleService _roleService = roleService;

    [HttpGet]
    public IActionResult Get()
    {
        var users = _userService.Get();

        return Ok(users.Select(user => new
        {
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

        return Ok(new
        {
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

        try
        {
            _userService.Create(auth0ID, userDTO.Name);
            Log.Information("User {Auth0ID} has been created with Name {Name}", auth0ID, userDTO.Name);
            return Ok(new { IsNew = true });
        }
        catch (AlmDbException)
        {
            return Ok(new { IsNew = false });
        }
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
