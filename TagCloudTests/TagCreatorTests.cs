using FluentAssertions;
using NSubstitute;
using TagCloud.Settings;
using TagCloud.WordCounter;

namespace TagCloudTests;

[TestFixture]
public class TagCreatorTests : BaseTest<TagCreator>
{
	public override void SetUp()
	{
		base.SetUp();
		Mock<IImageSettingsProvider>()
			.ImageSettings
			.Returns(new ImageSettings { FontSizeMax = 40, FontSizeMin = 10 });
	}

	[Test]
	public void CreateTags_ShouldReturnEmptyList_WhenNoWordsProvided()
	{
		var words = Enumerable.Empty<string>();
		var tags = Sut.CreateTags(words);
		tags.Should().BeNullOrEmpty();
	}

	[Test]
	public void CreateTags_ShouldCalculateTagWeightsCorrectly()
	{
		var words = new[]
			{ "apple", "banana", "apple", "cherry", "banana", "banana" }; // apple: 2, banana: 3, cherry: 1
		var tags = Sut.CreateTags(words);

		tags.Count.Should().Be(3);
		var appleTag = tags.Single(t => t.Word == "apple");
		var bananaTag = tags.Single(t => t.Word == "banana");
		var cherryTag = tags.Single(t => t.Word == "cherry");

		appleTag.Count.Should().Be(2);
		bananaTag.Count.Should().Be(3);
		cherryTag.Count.Should().Be(1);

		cherryTag.Weight.Should().Be(10);
		bananaTag.Weight.Should().Be(40);
		appleTag.Weight.Should().BeGreaterThan(10).And.BeLessThan(40);
	}
}