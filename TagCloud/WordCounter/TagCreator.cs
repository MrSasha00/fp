using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;

namespace TagCloud.WordCounter;

public class TagCreator(IImageSettingsProvider imageSettingsProvider) : ITagCreator
{
	public Result<List<Tag>> CreateTags(IEnumerable<string> words) =>
		words.GetCountInGroups()
			.Select(x => new Tag(x.Item, x.Count))
			.AsResult()
			.Then(tags => GetWeightTags(tags, imageSettingsProvider.ImageSettings.FontSizeMin,
				imageSettingsProvider.ImageSettings.FontSizeMax));

	private static Result<List<Tag>> GetWeightTags(IEnumerable<Tag> tags, int sizeMin, int sizeMax)
	{
		if(!tags.Any())
			return Result.Ok(new List<Tag>());

		tags = tags.ToList();
		var minCount = tags.Min(x => x.Count);
		var maxCount = tags.Max(x => x.Count);

		return tags.Select(tag => tag.WithWeight((int)(sizeMin + (double)(tag.Count - minCount) / (maxCount - minCount) * (sizeMax - sizeMin)))).ToList();
	}
}