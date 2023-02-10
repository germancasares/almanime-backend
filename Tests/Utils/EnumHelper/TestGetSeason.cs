using Almanime.Models.Enums;
using FluentAssertions;
using NUnit.Framework;
using Helper = Almanime.Utils.EnumHelper;

namespace Almanime.Tests.Utils.EnumHelper;

[TestFixture]
public class TestGetSeason
{
  [TestCase(1, ExpectedResult = ESeason.Winter)]
  [TestCase(2, ExpectedResult = ESeason.Winter)]
  [TestCase(3, ExpectedResult = ESeason.Spring)]
  [TestCase(4, ExpectedResult = ESeason.Spring)]
  [TestCase(5, ExpectedResult = ESeason.Spring)]
  [TestCase(6, ExpectedResult = ESeason.Summer)]
  [TestCase(7, ExpectedResult = ESeason.Summer)]
  [TestCase(8, ExpectedResult = ESeason.Summer)]
  [TestCase(9, ExpectedResult = ESeason.Fall)]
  [TestCase(10, ExpectedResult = ESeason.Fall)]
  [TestCase(11, ExpectedResult = ESeason.Fall)]
  [TestCase(12, ExpectedResult = ESeason.Winter)]
  public static ESeason IntegerInRange_ShouldReturnESeason(int month) => Helper.GetSeason(month);

  [Test]
  public static void IntegerOutOfRange_ShouldThrowException([Values(-1, 13, 9999)] int month) => month
    .Invoking(Helper.GetSeason)
    .Should()
    .Throw<Exception>()
    .WithMessage("Season out of range.");
}