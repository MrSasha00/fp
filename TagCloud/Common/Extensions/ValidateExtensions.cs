using TagCloud.Settings;

namespace TagCloud.Common.Extensions;

public static class ValidateExtensions
{
	public static Result<Settings.Settings> ValidateSettings(Settings.Settings settings)
	{
		if(string.IsNullOrEmpty(settings.AppSettings.SavePath))
			return Result.Fail<Settings.Settings>("SavePath is required");

		if(string.IsNullOrEmpty(settings.AppSettings.SourcePath))
			return Result.Fail<Settings.Settings>("SourcePath is required");

		return Result.Ok(settings);
	}

	public static Result<None> ValidateImageSettings(ImageSettings imageSettings)
	{
		if (imageSettings.Width <= 0 || imageSettings.Height <= 0)
			return Result.Fail<None>("Image dimensions must be positive.");

		if (string.IsNullOrEmpty(imageSettings.FontFamily))
			return Result.Fail<None>("FontFamily is not specified.");

		return Result.Ok();
	}
}