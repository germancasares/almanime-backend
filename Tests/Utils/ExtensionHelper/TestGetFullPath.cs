using Almanime.Utils;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Almanime.Tests.Utils.ExtensionHelper;

[TestFixture]
public static class TestGetFullPath
{
  [Test]
  public static void ValidHttpRequestShouldReturnUri()
  {
    // Arrange
    var request = new Mock<HttpRequest>();
    request.Setup(x => x.Scheme).Returns($"Scheme");
    request.Setup(x => x.Host).Returns(new HostString("Host"));
    request.Setup(x => x.PathBase).Returns($"/PathBase");
    request.Setup(x => x.Path).Returns($"/Path");

    // Act
    var path = request.Object.GetFullPath();

    // Assert
    path.Should().Be("Scheme://Host/PathBase/Path");
  }
}