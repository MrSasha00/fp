using TagCloud.CloudPainter;
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
	public void Run(AppSettings appSettings, ImageSettings imageSettings)
	{
		ValidateAppSettings(appSettings);
		imageSettingsProvider.ImageSettings = imageSettings;
		appSettingsProvider.AppSettings = appSettings;

		var words = wordPreprocessor.Process().ToArray();
		var tags = tagCreator.CreateTags(words);
		tags = tagPositioner.Position(tags);
		cloudPainter.Paint(tags.ToArray());
	}

	private void ValidateAppSettings(AppSettings appSettings)
	{
		if(string.IsNullOrEmpty(appSettings.SavePath))
			throw new ArgumentException("SavePath is required");

		if(string.IsNullOrEmpty(appSettings.SourcePath))
			throw new ArgumentException("SourcePath is required");
	}
}