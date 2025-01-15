using TagCloud.Settings;

namespace TagCloud.WordCounter;

public class TagCreator(IImageSettingsProvider imageSettingsProvider) : ITagCreator
{
	public List<Tag> CreateTags(IEnumerable<string> words)
	{
		if (!words.Any())
			return [..Array.Empty<Tag>()];

		var tags = words.GetCountInGroups()
			.Select(x =>
				new Tag
				{
					Count = x.Count,
					Word = x.Item
				})
			.ToList();

		var minCount = tags.Min(x => x.Count);
		var maxCount = tags.Max(x => x.Count);
		var minFontSize = imageSettingsProvider.ImageSettings.FontSizeMin;
		var maxFontSize = imageSettingsProvider.ImageSettings.FontSizeMax;

		foreach (var tag in tags)
		{
			tag.Weight = (int)(minFontSize + (double)(tag.Count - minCount) / (maxCount - minCount) * (maxFontSize - minFontSize));
		}

		return tags;
	}
}