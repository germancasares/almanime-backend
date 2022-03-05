using Almanime.Models;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;

namespace Almanime.Services;

public class UserService : IUserService
{
    private readonly AlmanimeContext _context;

    public UserService(AlmanimeContext context)
    {
        _context = context;
    }

    public User GetByAuth0ID(string auth0ID) => _context.Users.GetByAuth0ID(auth0ID);
    public IQueryable<User> Get() => _context.Users.AsQueryable();

    public void Create(string auth0Id, string name)
    {
        var user = _context.Users.SingleOrDefault(user => user.Auth0ID == auth0Id);
        if (user != null) return;

        _context.Users.Add(new User(auth0Id, name));
        _context.SaveChanges();
    }

    public void Update(string auth0ID, string name)
    {
        var user = _context.Users.GetByAuth0ID(auth0ID);

        user.Name = name;

        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
