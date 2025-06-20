﻿using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Almanime.Utils.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class MaxAttribute(double max) : DataTypeAttribute("max")
{
    public double Max { get; private set; } = max;

    public override string FormatErrorMessage(string name)
    {
        if (ErrorMessage == null && ErrorMessageResourceName == null)
        {
            ErrorMessage = "The field {0} must be less than or equal to {1}";
        }

        return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Max);
    }

    public override bool IsValid(object? value)
    {
        if (value == null) return true;

        var isDouble = double.TryParse(Convert.ToString(value), out var valueAsDouble);

        return isDouble && valueAsDouble <= Max;
    }
}
