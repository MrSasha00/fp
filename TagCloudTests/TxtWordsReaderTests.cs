using FluentAssertions;
using TagCloud.WordsReader;

namespace TagCloudTests;

[TestFixture]
public class TxtWordsReaderTests
{
	private TxtWordsReader _txtWordsReader;

	[SetUp]
	public void SetUp() =>
		_txtWordsReader = new TxtWordsReader();

	[Test]
	public void Read_ShouldNotBeNullOrEmpty()
	{
		var words = _txtWordsReader.Read(TestConstants.SamplesWordsTestFile);
		words.Should().NotBeNullOrEmpty();
	}

	[Test]
	public void Read_ShouldThrowExceptionIfFileDoesNotExist()
	{
		var act = () => _txtWordsReader.Read("");
		act.Should().Throw<Exception>();
	}
}