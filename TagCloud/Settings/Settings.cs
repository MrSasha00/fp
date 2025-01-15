namespace TagCloud.Settings;

public class Settings
{
	public AppSettings AppSettings { get; init; } = new();
	public ImageSettings ImageSettings { get; init; } = new();
}