namespace TagCloud.Settings;

public record ImageSettings
{
	public int Width { get; init; }
	public int Height { get; init; }
	public string? BackgroundColor { get; init; }
	public string? FontFamily { get; init; }
	public int FontSizeMax { get; init; }
	public int FontSizeMin { get; init; }
}