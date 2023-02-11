using Almanime.Models;

namespace Almanime.Services.Interfaces;

public interface IUserService
{
  void Create(string auth0Id, string name);
  IQueryable<User> Get();
  User GetByAuth0ID(string auth0ID);
  void Update(string auth0Id, string name);
}
