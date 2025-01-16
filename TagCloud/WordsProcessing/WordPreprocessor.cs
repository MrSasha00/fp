using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;

namespace TagCloud.WordsProcessing;

internal class WordPreprocessor(
	IBoringWordsProvider boringWordsProvider,
	IAppSettingsProvider appSettingsProvider)
	: IWordPreprocessor
{
	public Result<string[]> Process(string[] strings)
	{
		if (appSettingsProvider.AppSettings.SourcePath == null)
			return Result.Fail<string[]>("Source path is required");

		return boringWordsProvider
			.GetWords()
			.Then(boringWords => ProcessWords(strings, boringWords));
	}

	private static Result<string[]> ProcessWords(string[] word, string[] boringWords) =>
		word
			.Select(x => x.ToLower())
			.Where(x => !boringWords.Contains(x))
			.ToArray()
			.AsResult();
}