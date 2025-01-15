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
}