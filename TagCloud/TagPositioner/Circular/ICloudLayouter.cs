using System.Drawing;

namespace TagCloud.TagPositioner.Circular;

public interface ICloudLayouter
{
	Rectangle PutNextRectangle(Size rectangleSize, ICollection<Rectangle> rectangles);
}