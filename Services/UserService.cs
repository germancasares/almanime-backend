using Almanime.Models;
using Almanime.Repositories;
using Almanime.Services.Interfaces;

namespace Almanime.Services;

public class UserService : IUserService
{
    private readonly AlmanimeContext _context;

    public UserService(AlmanimeContext context)
    {
        _context = context;
    }
    public void Create(string auth0Id, string name)
    {
        var user = _context.Users.SingleOrDefault(user => user.Auth0ID == auth0Id);

        if (user != null) return;

        user = _context.Users.Add(new User(auth0Id, name)).Entity;

        _context.SaveChanges();
    }
}
