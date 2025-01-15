using TagCloud.Common;

namespace TagCloud.WordsReader;

public interface IWordsReader
{
	Result<string[]> Read(string path);
}