using TagCloud.Common;
using TagCloud.Common.Extensions;

namespace TagCloud.WordsReader;

public class TxtWordsReader : IWordsReader
{
	public Result<string[]> Read(string path)
	{
		try
		{
			var lines = File.ReadLines(path).ToArray();
			return lines.AsResult();
		}
		catch (Exception)
		{
			return Result.Fail<string[]>($"Во время чтения файла {path} произошла ошибка.");
		}
	}
}