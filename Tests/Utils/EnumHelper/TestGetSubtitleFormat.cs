using Almanime.Models;
using Almanime.Models.Enums;
using Almanime.Utils;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Almanime.Tests.Utils.EnumHelper;

[TestFixture]
public static class TestGetSubtitleFormat
{
    private static IFormFile GetMockFileWithExtension(string extension)
    {
        var file = new Mock<IFormFile>();
        file.Setup(x => x.FileName).Returns($"filename{extension}");

        return file.Object;
    }

    [TestCase(".ass", ESubtitleFormat.ASS)]
    [TestCase(".srt", ESubtitleFormat.SRT)]
    public static void FileWithSubtitleExtension_ShouldReturnESubtitleFormat(string extension, ESubtitleFormat expectedFormat)
    {
        // Arrange
        var formFile = GetMockFileWithExtension(extension);

        // Act
        var format = formFile.GetSubtitleFormat();

        // Assert
        format.Should().Be(expectedFormat);
    }

    [Test]
    public static void FileWithOtherExtension_ShouldThrowException([Values(".txt", ".png", ".jpg")] string extension)
    {
        // Arrange
        var formFile = GetMockFileWithExtension(extension);

        // Act & Assert
        formFile
          .Invoking(file => file.GetSubtitleFormat())
          .Should()
          .Throw<AlmValidationException>()
          .WithMessage("file breaks rule FormatNotValid");
    }
}