using TagCloud.Settings;
using TagCloud.WordsReader;

namespace TagCloud.WordsProcessing;

public class FIleBoringWordsProvider(IAppSettingsProvider appSettingsProvider, IWordsReader wordsReader)
	: IBoringWordsProvider
{
	public string[] GetWords() =>
		string.IsNullOrEmpty(appSettingsProvider.AppSettings.BoringWordsPath)
			? []
			: wordsReader.Read(appSettingsProvider.AppSettings.BoringWordsPath);
}