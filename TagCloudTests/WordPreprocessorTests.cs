using FluentAssertions;
using NSubstitute;
using TagCloud.Settings;
using TagCloud.WordsProcessing;
using TagCloud.WordsReader;

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
	public void Process_WordsShouldFilter()
	{
		Mock<IBoringWordsProvider>()
			.GetWords()
			.Returns(["в", "на"]);
		Mock<IWordsReader>()
			.Read(Arg.Any<string>())
			.Returns(["в", "на", "привет"]);

		var processedWords = Sut.Process();

		processedWords.Should().NotBeNullOrEmpty();
		processedWords.Length.Should().Be(1);
		processedWords.First().Should().Be("привет");
	}

	[Test]
	public void Process_WordsShouldBeLowercase()
	{
		Mock<IWordsReader>()
			.Read(Arg.Any<string>())
			.Returns(["ПРИВЕТ"]);

		var processedWords = Sut.Process();

		processedWords.First().Should().Be("привет");
	}
}