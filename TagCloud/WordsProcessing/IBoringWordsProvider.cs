using TagCloud.Common;

namespace TagCloud.WordsProcessing;

public interface IBoringWordsProvider
{
	Result<string[]> GetWords();
}