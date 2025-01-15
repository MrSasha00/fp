using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using TagCloud.Settings;

namespace TagCloud.TagPositioner.Circular;

public class CircularCloudLayouter(IImageSettingsProvider imageSettingsProvider) : ICloudLayouter
{
	private Point _center;
	private double _angle;
	private const double SpiralStep = 0.2;
	private const double AngleStep = 0.01;

	public Rectangle PutNextRectangle(Size rectangleSize, ICollection<Rectangle> rectangles)
	{
		_center = new Point(imageSettingsProvider.ImageSettings.Width / 2,
			imageSettingsProvider.ImageSettings.Height / 2);
		Rectangle newRectangle;
		if (!rectangles.Any())
		{
			var rectangle = new Rectangle(_center, rectangleSize);
			rectangles.Add(rectangle);
			return rectangle;
		}

		do
		{
			var location = GetLocation(rectangleSize);
			newRectangle = new Rectangle(location, rectangleSize);
		} while (rectangles.IsIntersecting(newRectangle));

		rectangles.Add(newRectangle);

		return newRectangle;
	}

	[SuppressMessage("ReSharper", "PossibleLossOfFraction")]
	private Point GetLocation(Size rectangleSize)
	{
		var radius = SpiralStep * _angle;
		var x = (int)(_center.X + radius * Math.Cos(_angle) - rectangleSize.Width / 2);
		var y = (int)(_center.Y + radius * Math.Sin(_angle) - rectangleSize.Height / 2);
		_angle += AngleStep;

		return new Point(x, y);
	}
}