using System.Drawing;

namespace TagCloud.Common.Extensions;

public static class GraphicsExtensions
{
	public static Result<(Graphics graphics, Bitmap bitmap)> CreateGraphics(int width, int height)
	{
		try
		{
			var bitmap = new Bitmap(width, height);
			var graphics = Graphics.FromImage(bitmap);

			return (graphics, bitmap);
		}
		catch (Exception ex)
		{
			return Result.Fail<(Graphics, Bitmap)>($"Failed to create graphics: {ex.Message}");
		}
	}

	public static Result<Font> CreateFont(string fontFamily, int weight)
	{
		try
		{
			return Result.Ok(new Font(fontFamily, weight));
		}
		catch (Exception ex)
		{
			return Result.Fail<Font>($"Failed to create font: {ex.Message}");
		}
	}
}