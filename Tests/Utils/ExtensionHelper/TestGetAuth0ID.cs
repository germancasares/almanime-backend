using Almanime.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Security.Claims;
using static Almanime.Utils.ExtensionHelper;

namespace Almanime.Tests.Utils.ExtensionHelper;

[TestFixture]
public static class TestGetIdentityID
{
  [Test]
  public static void ValidNameIdentifier_ShouldReturnGuid([Values("00000000-0000-0000-0000-000000000000", "B70DDB8F-892B-4E56-886F-A7F253F8A56B")] string nameIdentifier)
  {
    // Arrange
    var claims = new List<Claim>()
    {
      new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
    };
    var identity = new ClaimsIdentity(claims, "TestAuthType");
    var user = new ClaimsPrincipal(identity);

    // Act
    var ID = user.GetAuth0ID();

    // Assert
    ID.Should().Be(nameIdentifier);
  }

  [Test]
  public static void InvalidNameIdentifier_ShouldThrowException([Values("", "random", "3213215324314213", "-1", "@@@@")] string nameIdentifier)
  {
    // Arrange
    var claims = new List<Claim>()
    {
      new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
    };
    var identity = new ClaimsIdentity(claims, "TestAuthType");
    var user = new ClaimsPrincipal(identity);

    // Act & Assert
    user.Invoking(user => user.GetAuth0ID())
      .Should().Throw<AlmNullException>()
      .WithMessage("auth0ID breaks rule NotEmpty");
  }
}