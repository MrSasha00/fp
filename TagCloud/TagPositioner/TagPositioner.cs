using System.Drawing;
using TagCloud.Settings;
using TagCloud.TagPositioner.Circular;
using TagCloud.WordCounter;

namespace TagCloud.TagPositioner;

public class TagPositioner(ICloudLayouter cloudLayouter, IImageSettingsProvider imageSettingsProvider)
	: ITagPositioner
{
	public List<Tag> Position(List<Tag> tags)
	{
		var size = new Size(imageSettingsProvider.ImageSettings.Width, imageSettingsProvider.ImageSettings.Height);
		var bitmap = new Bitmap(size.Width, size.Height);
		using var graphics = Graphics.FromImage(bitmap);

		var fontFamily = new FontFamily(imageSettingsProvider.ImageSettings.FontFamily ?? "Arial");
		var rectangles = new List<Rectangle>();

		return (
			from tag in tags
			let font = new Font(fontFamily, tag.Weight)
			let textSize = graphics.MeasureString(tag.Word, font)
			let res = cloudLayouter.PutNextRectangle(new Size((int)textSize.Width, (int)textSize.Height), rectangles)
			select tag.WithLocation(new Point(res.X, res.Y))).ToList();
	}
}