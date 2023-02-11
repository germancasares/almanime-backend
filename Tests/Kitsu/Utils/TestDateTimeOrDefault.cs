using FluentAssertions;
using NUnit.Framework;
using Helper = Almanime.Kitsu.Utils;

namespace Almanime.Tests.Kitsu.Utils;

[TestFixture]
public class TestDateTimeOrDefault
{
  [Test]
  public static void StringIsValid_ShouldReturnDateTime()
  {
    // Arrange
    var date = "2023-01-01";

    // Act
    var datetime = Helper.DateTimeOrDefault(date);

    // Assert
    datetime.Should().Be(new DateTime(2023, 1, 1));
  }

  [TestCase(null, ExpectedResult = null)]
  [TestCase("", ExpectedResult = null)]
  [TestCase("2023/01/01", ExpectedResult = null)]
  [TestCase("dsadsadsads", ExpectedResult = null)]
  public static DateTime? StringIsValid_ShouldReturnDefault(string date) => Helper.DateTimeOrDefault(date);
}