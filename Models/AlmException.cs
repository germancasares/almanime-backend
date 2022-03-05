using Almanime.Models.Enums;
using Domain.Enums;

namespace Almanime.Models;

public abstract class AlmException : Exception
{
    public EValidationCode Code { get; set; }
    public string Rule => Code.ToString();
    public string Field { get; set; }

    protected AlmException(
        EValidationCode code,
        string field
    ) : base($"{field} breaks rule {code}")
    {
        Code = code;
        Field = field;
    }
}

public class AlmNullException : AlmException
{
    public AlmNullException(string field) : base(EValidationCode.NotEmpty, field) {}
}

public class AlmDbException : AlmException
{
    public Dictionary<string, object> QueryParams { get; set; }
    public AlmDbException(EValidationCode code, string field, Dictionary<string, object> queryParams) : base(code, field)
    {
        QueryParams = queryParams;
    }
}

public class AlmPermissionException : AlmException
{
    public string Permission { get; set; }
    public string UserName { get; set; }
    public string FansubName { get; set; }

    public AlmPermissionException(EPermission permission, string userName, string fansubName) : base(EValidationCode.DoesntHavePermission, "permission")
    {
        Permission = permission.ToString();
        UserName = userName;
        FansubName = fansubName;
    }
}
