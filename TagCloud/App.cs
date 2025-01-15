using TagCloud.CloudPainter;
using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;
using TagCloud.TagPositioner;
using TagCloud.WordCounter;
using TagCloud.WordsProcessing;
using TagCloud.WordsReader;

namespace TagCloud;

internal class App(
	IWordsReader wordsReader,
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
			.Then(SetSettingProviders)
			.Then(_ => wordsReader.Read(settings.AppSettings.SourcePath))
			.Then(wordPreprocessor.Process)
			.Then(tagCreator.CreateTags)
			.Then(tagPositioner.Position)
			.Then(cloudPainter.Paint)
			.RefineError("Error while generating cloud")
			.OnFail(error => throw new ApplicationException(error));
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