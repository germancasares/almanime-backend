using Almanime.Models.Enums;
using NUnit.Framework;
using Helper = Almanime.Utils.EnumHelper;

namespace Almanime.Tests.Utils.EnumHelper;

[TestFixture]
public class TestGetEnumFromString
{
  [TestCase("Winter", ExpectedResult = ESeason.Winter)]
  [TestCase("winter", ExpectedResult = null)]
  [TestCase("dsadsadsa", ExpectedResult = null)]
  [TestCase(null, ExpectedResult = null)]
  public static ESeason? StringIsValidShouldReturnEnum(string? value) =>
    Helper.GetEnumFromString<ESeason>(value);
}