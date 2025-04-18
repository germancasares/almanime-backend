using Almanime.Models;
using Almanime.Models.Enums;
using Almanime.Repositories;
using Almanime.Repositories.Queries;
using Almanime.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public void Create(string auth0ID, string name)
    {
        var user = _context.Users.SingleOrDefault(user => user.Auth0ID == auth0ID);
        if (user != null) throw new AlmDbException(EValidationCode.AlreadyInDB, nameof(user), new()
    {
      { nameof(auth0ID), auth0ID },
      { nameof(name), name },
    });

        _context.Users.Add(new User(auth0ID, name));
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException)
        {
            throw new AlmDbException(EValidationCode.AlreadyInDB, nameof(user), new()
      {
        { nameof(auth0ID), auth0ID },
        { nameof(name), name },
      });
        }
    }

    public void Update(string auth0Id, string name)
    {
        var user = _context.Users.GetByAuth0ID(auth0Id);

        user.Name = name;

        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
