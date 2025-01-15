using FluentAssertions;
using NSubstitute;
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
			.Returns(["some", "word"]);

		var strings = Sut.GetWords();

		strings.Should().NotBeNullOrEmpty();
	}

	[Test]
	public void GetWords_ShouldBeEmpty_WhenPathIsNull()
	{
		Mock<IAppSettingsProvider>()
			.AppSettings
			.Returns(new AppSettings());

		var strings = Sut.GetWords();

		strings.Should().BeEmpty();
	}
}