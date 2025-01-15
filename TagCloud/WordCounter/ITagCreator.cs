namespace TagCloud.WordCounter;

public interface ITagCreator
{
	List<Tag> CreateTags(IEnumerable<string> words);
}