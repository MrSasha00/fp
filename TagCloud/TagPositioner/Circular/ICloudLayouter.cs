using System.Drawing;
using TagCloud.Common;

namespace TagCloud.TagPositioner.Circular;

public interface ICloudLayouter
{
	Result<Rectangle> PutNextRectangle(Size rectangleSize, ICollection<Rectangle> rectangles);
}