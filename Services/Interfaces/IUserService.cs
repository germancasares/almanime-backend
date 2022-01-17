using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface IUserService
{
    void Create(string auth0Id, string name);
    IQueryable<User> Get();
}
