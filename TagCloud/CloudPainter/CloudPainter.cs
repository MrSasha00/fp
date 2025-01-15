using System.Drawing;
using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;
using TagCloud.WordCounter;

namespace TagCloud.CloudPainter;

internal class CloudPainter(IImageSettingsProvider imageSettingsProvider,
	IAppSettingsProvider appSettingsProvider,
	IImageSaver imageSaver)
	: ICloudPainter
{
	public Result<None> Paint(IEnumerable<Tag> tags)
	{
		ValidateSavePath()
			.Then(_ => ValidateExtensions.ValidateImageSettings(imageSettingsProvider.ImageSettings))
			.Then(_ => GraphicsExtensions.CreateGraphics(imageSettingsProvider.ImageSettings.Width, imageSettingsProvider.ImageSettings.Height))
			.Then(paint => DrawTags(paint, tags))
			.Then(paint => SaveImage(paint.b))
			.RefineError("Error occurred while drawing tags");

		return new None();
	}

	private Result<None> ValidateSavePath() =>
		string.IsNullOrEmpty(appSettingsProvider.AppSettings.SavePath) ? Result.Fail<None>("Save path is required") : Result.Ok();

	private Result<(Graphics gr, Bitmap b)> DrawTags((Graphics Graphics, Bitmap Bitmap) paint, IEnumerable<Tag> tags)
	{
		var result = ClearBitmap(paint)
			.Then(_ => tags
				.Select(t => DrawSingleTag(paint, t))
				.SplitResults());

		return !result.IsSuccess ? Result.Fail<(Graphics gr, Bitmap b)>(result.Error) : Result.Ok(paint);
	}

	private Result<None> DrawSingleTag((Graphics Graphics, Bitmap Bitmap) paint, Tag tag)
	{
		var random = new Random();

		return GraphicsExtensions.CreateFont(imageSettingsProvider.ImageSettings.FontFamily, tag.Weight)
			.Then(font =>
			{
				var color = Color.FromArgb(random.Next(100, 256), random.Next(100, 256), random.Next(100, 256));
				using var brush = new SolidBrush(color);
				paint.Graphics.DrawString(tag.Word, font, brush, tag.Location);
				return Result.Ok();
			})
			.RefineError($"Failed to draw tag '{tag.Word}'");;
	}

	private Result<None> ClearBitmap((Graphics Graphics, Bitmap Bitmap) paint)
	{
		var backgroundColor = imageSettingsProvider.ImageSettings.BackgroundColor ?? "white";
		paint.Graphics.Clear(Color.FromName(backgroundColor));

		return Result.Ok();
	}

	private Result<None> SaveImage(Bitmap bitmap)
	{
		try
		{
			imageSaver.SaveImage(bitmap, appSettingsProvider.AppSettings.SavePath);
			return Result.Ok();
		}
		catch (Exception e)
		{
			return Result.Fail<None>($"Save image failed:{e.Message}");
		}
	}
}