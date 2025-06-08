using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Almanime.Utils.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class MinAttribute(double min) : DataTypeAttribute("min")
{
    public double Min { get; private set; } = min;

    public override string FormatErrorMessage(string name)
    {
        if (ErrorMessage == null && ErrorMessageResourceName == null)
        {
            ErrorMessage = "The field {0} must be more than or equal to {1}";
        }

        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Min);
    }

    public override bool IsValid(object? value)
    {
        if (value == null) return true;

        var isDouble = double.TryParse(Convert.ToString(value), out var valueAsDouble);

        return isDouble && valueAsDouble >= Min;
    }
}
