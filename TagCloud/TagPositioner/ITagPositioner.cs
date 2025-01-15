using TagCloud.Common;
using TagCloud.WordCounter;

namespace TagCloud.TagPositioner;

public interface ITagPositioner
{
	Result<IEnumerable<Tag>> Position(List<Tag> tags);
}