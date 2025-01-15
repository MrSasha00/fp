using System.Drawing;

namespace TagCloud.TagPositioner.Circular;

public static class CircularCloudLayouterExtensions
{
	public static bool IsIntersecting(this IEnumerable<Rectangle> rectangles, Rectangle rectangle)
		=> rectangles.Any(existingRectangle => existingRectangle.IntersectsWith(rectangle));
}