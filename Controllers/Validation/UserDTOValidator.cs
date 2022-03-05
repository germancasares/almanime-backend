using Almanime.Models.DTO;
using Almanime.Models.Enums;
using Almanime.Repositories;
using FluentValidation;

namespace Almanime.Controllers.Validation;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator(AlmanimeContext context)
    {
        When(r => r.Name != null, () =>
        {
            RuleFor(r => r.Name)
                .Must(name => !context.Users.Any(user => user.Name == name))
                .WithMessage(EValidationCode.Unique.ToString());
        });
    }
}
