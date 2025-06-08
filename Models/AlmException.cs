using Almanime.Models.Enums;

namespace Almanime.Models;

public abstract class AlmException(
    EValidationCode code,
    string field
    ) : Exception($"{field} breaks rule {code}")
{
    public EValidationCode Code { get; set; } = code;
    public string Rule => Code.ToString();
    public string Field { get; set; } = field;
}

public class AlmValidationException(EValidationCode code, string field) : AlmException(code, field)
{
}

public class AlmNullException(string field) : AlmException(EValidationCode.NotEmpty, field)
{
}

public class AlmDbException(EValidationCode code, string field, Dictionary<string, object> queryParams) : AlmException(code, field)
{
    public Dictionary<string, object> QueryParams { get; set; } = queryParams;
}

public class AlmPermissionException(EPermission permission, string userName, string fansubName) : AlmException(EValidationCode.DoesntHavePermission, "permission")
{
    public string Permission { get; set; } = permission.ToString();
    public string UserName { get; set; } = userName;
    public string FansubName { get; set; } = fansubName;
}
