using System.Drawing;

namespace TagCloud.WordCounter;

public class Tag
{
	public string Word { get; private set; }
	public int Count { get; private set; }
	public int Weight { get; private set; }
	public Point Location { get; private set; }

	public Tag(string word, int count, int weight, Point location)
	{
		Word = word;
		Count = count;
		Weight = weight;
		Location = location;
	}

	public Tag(string word, int count)
	{
		Word = word;
		Count = count;
	}

	public Tag WithWeight(int weight)
	{
		return new Tag(Word, Count, weight, Location);
	}

	public Tag WithLocation(Point location)
	{
		return new Tag(Word, Count, Weight, location);
	}
}