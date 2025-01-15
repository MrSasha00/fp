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
		var result = _txtWordsReader.Read(TestConstants.SamplesWordsTestFile);
		result.Value.Should().NotBeNullOrEmpty();
	}

	[Test]
	public void Read_ShouldThrowExceptionIfFileDoesNotExist()
	{
		var result = _txtWordsReader.Read("");
		result.Error.Should().NotBeNullOrEmpty();
		result.IsSuccess.Should().BeFalse();
	}
}