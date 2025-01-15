namespace TagCloud.WordsReader;

public class TxtWordsReader : IWordsReader
{
	public string[] Read(string path)
	{
		try
		{
			return File.ReadLines(path).ToArray();
		}
		catch (Exception e)
		{
			throw new Exception($"Во время чтения файла {path} произошла ошибка.", e);
		}
	}
}