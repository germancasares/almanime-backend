using Almanime.Models;
using Almanime.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Almanime.Repositories.Queries;

public static class UserQueries
{
    public static User GetByAuth0ID(this DbSet<User> users, string auth0ID)
    {
        var user = users.SingleOrDefault(user => user.Auth0ID == auth0ID);
        if (user == null)
        {
            throw new AlmDbException(EValidationCode.DoesntExistInDB, nameof(user), new()
            {
                { nameof(auth0ID), auth0ID },
            });
        }

        return user;
    }
}
