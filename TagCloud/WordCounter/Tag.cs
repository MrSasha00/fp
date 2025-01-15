using System.Drawing;

namespace TagCloud.WordCounter;

public class Tag
{
	public string Word { get; set; } = string.Empty;
	public int Count { get; set; }
	public int Weight { get; set; }
	public Point Location { get; set; }
}