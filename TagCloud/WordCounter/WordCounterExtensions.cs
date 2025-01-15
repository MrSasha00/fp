namespace TagCloud.WordCounter;

public static class WordCounterExtensions
{
	public static IEnumerable<(T Item, int Count)> GetCountInGroups<T>(this IEnumerable<T> source) =>
		source
			.GroupBy(x => x)
			.Select(x => (x.Key, x.Count()));
}