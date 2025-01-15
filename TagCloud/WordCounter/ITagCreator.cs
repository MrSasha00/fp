using TagCloud.Common;

namespace TagCloud.WordCounter;

public interface ITagCreator
{
	Result<List<Tag>> CreateTags(IEnumerable<string> words);
}