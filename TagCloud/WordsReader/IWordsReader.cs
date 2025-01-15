namespace TagCloud.WordsReader;

public interface IWordsReader
{
	string[] Read(string path);
}