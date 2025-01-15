using FluentAssertions;
using NSubstitute;
using TagCloud.Common.Extensions;
using TagCloud.Settings;
using TagCloud.WordsProcessing;

namespace TagCloudTests;

internal class WordPreprocessorTests : BaseTest<WordPreprocessor>
{
	[SetUp]
	public override void SetUp()
	{
		base.SetUp();

		Mock<IAppSettingsProvider>()
			.AppSettings
			.Returns(new AppSettings { SourcePath = "Path" });
	}

	[Test]
	public void Process_WordsShouldFilter_WhenWordsNotEmpty()
	{
		Mock<IBoringWordsProvider>()
			.GetWords()
			.Returns(Result.Ok<string[]>(["в", "на"]));

		var result = Sut.Process(["в", "на", "привет"]);

		result.Value.Should().NotBeNullOrEmpty();
		result.IsSuccess.Should().BeTrue();
		result.Value.Length.Should().Be(1);
		result.Value.First().Should().Be("привет");
	}

	[Test]
	public void Process_WordsShouldBeLowercase_WhenWordsNotEmpty()
	{
		Mock<IBoringWordsProvider>()
			.GetWords()
			.Returns(Result.Ok(Array.Empty<string>()));

		var result = Sut.Process(["ПРИВЕТ"]);

		result.Value.First().Should().Be("привет");
		result.IsSuccess.Should().BeTrue();
	}

	[Test]
	public void Process_ResultShouldBeFail_WhenAppSettingsIsNull()
	{
		Mock<IAppSettingsProvider>()
			.AppSettings
			.Returns(new AppSettings());

		var result = Sut.Process([""]);

		result.IsSuccess.Should().BeFalse();
	}
}