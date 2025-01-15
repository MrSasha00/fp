using FluentAssertions;
using NSubstitute;
using TagCloud.Common.Extensions;
using TagCloud.Settings;
using TagCloud.WordsProcessing;
using TagCloud.WordsReader;

namespace TagCloudTests;

public class FileBoringWordsProviderTests : BaseTest<FIleBoringWordsProvider>
{
	[Test]
	public void GetWords_ShouldBeNotNullOrEmpty_WhenPathIsNotEmpty()
	{
		Mock<IAppSettingsProvider>()
			.AppSettings
			.Returns(new AppSettings {BoringWordsPath = "SomePath"});
		Mock<IWordsReader>()
			.Read(Arg.Any<string>())
			.Returns(Result.Ok<string[]>(["some", "word"]));

		var strings = Sut.GetWords();

		strings.Value.Should().NotBeNullOrEmpty();
		strings.IsSuccess.Should().BeTrue();
	}

	[Test]
	public void GetWords_ShouldBeEmpty_WhenPathIsNull()
	{
		Mock<IAppSettingsProvider>()
			.AppSettings
			.Returns(new AppSettings());

		var strings = Sut.GetWords();

		strings.Value.Should().BeEmpty();
		strings.IsSuccess.Should().BeTrue();
	}

	[Test]
	public void GetWords_ShouldBeFail_WhenReadIsFail()
	{
		Mock<IAppSettingsProvider>()
			.AppSettings
			.Returns(new AppSettings {BoringWordsPath = "SomePath"});
		Mock<IWordsReader>()
			.Read(Arg.Any<string>())
			.Returns(Result.Fail<string[]>("error"));

		var strings = Sut.GetWords();

		strings.Value.Should().BeNullOrEmpty();
		strings.IsSuccess.Should().BeFalse();
		strings.Error.Should().NotBeNullOrEmpty();
	}
}