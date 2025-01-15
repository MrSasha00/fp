using System.Drawing;
using FluentAssertions;
using NSubstitute;
using TagCloud.Settings;
using TagCloud.TagPositioner;
using TagCloud.TagPositioner.Circular;
using TagCloud.WordCounter;

namespace TagCloudTests;


public class TagPositionerTests : BaseTest<TagPositioner>
{
	public override void SetUp()
	{
		base.SetUp();
		Mock<IImageSettingsProvider>()
			.ImageSettings
			.Returns(new ImageSettings { FontFamily = "arial", Width = 500, Height = 500 });
	}

	[Test]
	public void Position_ShouldBeCorrect_WhenInputIsCorrect()
	{
		Mock<ICloudLayouter>()
			.PutNextRectangle(Arg.Any<Size>(), Arg.Any<List<Rectangle>>())
			.Returns(new Rectangle(1,2,3,4));

		var result = Sut.Position([new Tag("banana", 4, 10, Point.Empty)]);
		result.IsSuccess.Should().BeTrue();
		result.Value.Count().Should().Be(1);
		result.Value.First().Location.X.Should().Be(1);
		result.Value.First().Location.Y.Should().Be(2);
	}

	[Test]
	public void Position_ShouldBeFail_WhenFontIsBad()
	{
		Mock<IImageSettingsProvider>()
			.ImageSettings
			.Returns(new ImageSettings { FontFamily = "", Height = 100, Width = 100 });

		var result = Sut.Position([new Tag("banana", 4, 10, Point.Empty)]);
		result.IsSuccess.Should().BeFalse();
	}

	[Test]
	public void Position_ShouldBeFail_WhenTagIsBig()
	{
		Mock<ICloudLayouter>()
			.PutNextRectangle(Arg.Any<Size>(), Arg.Any<List<Rectangle>>())
			.Returns(new Rectangle(1,2,1000,1000));

		var result = Sut.Position([new Tag("banana", 4, 10, Point.Empty)]);
		result.IsSuccess.Should().BeFalse();
	}
}