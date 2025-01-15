using TagCloud.CloudPainter;
using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;
using TagCloud.TagPositioner;
using TagCloud.WordCounter;
using TagCloud.WordsProcessing;

namespace TagCloud;

internal class App(
	IWordPreprocessor wordPreprocessor,
	ITagCreator tagCreator,
	ICloudPainter cloudPainter,
	ITagPositioner tagPositioner,
	IImageSettingsProvider imageSettingsProvider,
	IAppSettingsProvider appSettingsProvider)
: IApp
{
	public void Run(Settings.Settings settings)
	{
		ValidateExtensions.ValidateSettings(settings)
			.Then(SetSettingProviders);

		var words = wordPreprocessor.Process().ToArray();
		var tags = tagCreator.CreateTags(words);
		tags = tagPositioner.Position(tags);
		cloudPainter.Paint(tags.ToArray());
	}

	private Result<None> SetSettingProviders(Settings.Settings settings)
	{
		try
		{
			appSettingsProvider.AppSettings = settings.AppSettings;
			imageSettingsProvider.ImageSettings = settings.ImageSettings;
			return Result.Ok(new None());
		}
		catch (Exception)
		{
			return Result.Fail<None>("Failed to set settings");
		}
	}
}