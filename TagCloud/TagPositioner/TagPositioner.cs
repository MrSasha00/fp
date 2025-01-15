using System.Drawing;
using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;
using TagCloud.TagPositioner.Circular;
using TagCloud.WordCounter;

namespace TagCloud.TagPositioner;

public class TagPositioner(ICloudLayouter cloudLayouter, IImageSettingsProvider imageSettingsProvider)
	: ITagPositioner
{
	public Result<IEnumerable<Tag>> Position(List<Tag> tags) =>
		ValidateExtensions.ValidateImageSettings(imageSettingsProvider.ImageSettings)
			.Then(_ => GraphicsExtensions.CreateGraphics(imageSettingsProvider.ImageSettings.Width, imageSettingsProvider.ImageSettings.Height))
			.Then(gr => PositionTags(gr, tags));

	private Result<IEnumerable<Tag>> PositionTags((Graphics Graphics, Bitmap Bitmap) paint, List<Tag> tags)
	{
		var rectangles = new List<Rectangle>();

		 return tags.Select(tag => ProcessTag(tag, paint, rectangles)).ToList()
			.SplitResults();
	}

	private Result<Tag> ProcessTag(Tag tag, (Graphics Graphics, Bitmap Bitmap) paint, List<Rectangle> rectangles) =>
		GraphicsExtensions.CreateFont(imageSettingsProvider.ImageSettings.FontFamily, tag.Weight)
			.Then(font => MeasureTextSize(tag, font, paint.Graphics))
			.Then(size => PutRectangle(size, paint.Bitmap, rectangles))
			.Then(rectangle => tag.WithLocation(rectangle.Location))
			.RefineError($"Error processing tag '{tag.Word}'.");

	private Result<SizeF> MeasureTextSize(Tag tag, Font font, Graphics graphics) =>
		graphics.MeasureString(tag.Word, font);

	private static Result<Rectangle> IsRectangleWithinBitmap(Rectangle rectangle, Bitmap bitmap)
	{
		if(rectangle is { Left: >= 0, Top: >= 0 }
			&& rectangle.Right <= bitmap.Width
			&& rectangle.Bottom <= bitmap.Height)
			return Result.Ok(rectangle);

		return Result.Fail<Rectangle>("Rectangle is not within the bitmap");
	}

	private Result<Rectangle> PutRectangle(SizeF textSize, Bitmap bitmap, List<Rectangle> rectangles)
	{
		var size = new Size((int)textSize.Width, (int)textSize.Height);
		return cloudLayouter.PutNextRectangle(size, rectangles)
			.Then(rectangle => IsRectangleWithinBitmap(rectangle, bitmap));
	}
}