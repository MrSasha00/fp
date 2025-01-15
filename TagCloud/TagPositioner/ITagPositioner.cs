using TagCloud.WordCounter;

namespace TagCloud.TagPositioner;

public interface ITagPositioner
{
	List<Tag> Position(List<Tag> tags);
}