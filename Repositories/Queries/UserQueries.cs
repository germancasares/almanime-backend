using Almanime.Models;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class UserQueries
{
    public static User? GetByAuth0ID(this DbSet<User> users, string? auth0ID)
    {
        if (auth0ID == null) return null;

        return users.SingleOrDefault(user => user.Auth0ID == auth0ID);
    }
}
