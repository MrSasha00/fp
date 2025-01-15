namespace TagCloud.Settings;

public record AppSettings
{
	public string? SourcePath { get; init; }
	public string? SavePath { get; init; }
	public string? BoringWordsPath { get; init; }
}