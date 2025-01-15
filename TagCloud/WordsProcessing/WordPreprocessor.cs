using TagCloud.Settings;
using TagCloud.WordsReader;

namespace TagCloud.WordsProcessing;

internal class WordPreprocessor(
	IBoringWordsProvider boringWordsProvider,
	IWordsReader wordsReader,
	IAppSettingsProvider appSettingsProvider)
	: IWordPreprocessor
{
	public string[] Process()
	{
		if (appSettingsProvider.AppSettings.SourcePath == null)
			throw new ArgumentException("Source path is required");

		var boringWords = boringWordsProvider.GetWords();
		return wordsReader.Read(appSettingsProvider.AppSettings.SourcePath)
			.Select(x => x.ToLower())
			.Where(x => !boringWords.Contains(x))
			.ToArray();
	}
}