namespace Almanime.Models.Enums;

public enum EValidationCode
{
  NotEmpty = 1,
  Unique = 2,
  DoesntExistInDB = 3,
  AlreadyInDB = 4,
  DoesntHavePermission = 5,
  FormatNotValid = 6,
}
