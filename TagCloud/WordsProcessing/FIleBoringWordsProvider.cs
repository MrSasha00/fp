using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;
using TagCloud.WordsReader;

namespace TagCloud.WordsProcessing;

public class FIleBoringWordsProvider(IAppSettingsProvider appSettingsProvider, IWordsReader wordsReader)
	: IBoringWordsProvider
{
	public Result<string[]> GetWords()
	{
		return string.IsNullOrEmpty(appSettingsProvider.AppSettings.BoringWordsPath)
			? Result.Ok<string[]>([])
			: wordsReader.Read(appSettingsProvider.AppSettings.BoringWordsPath);
	}
}