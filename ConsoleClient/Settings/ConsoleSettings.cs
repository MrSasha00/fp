using CommandLine;
using TagCloud.Settings;

namespace ConsoleClient.Settings;

public class ConsoleSettings
{
	[Option('w', "wight", Default = 800, HelpText = "Ширина изображения.")]
	public int Width { get; set; }

	[Option('h', "height", Default = 800, HelpText = "Высота изображения.")]
	public int Height { get; set; }

	[Option('b', "background", Default = "white", HelpText = "Цвет фона.")]
	public string BackgroundColor { get; set; } = string.Empty;

	[Option('f', "font", Default = "arial", HelpText = "Шрифт.")]
	public string FontFamily { get; set; } = string.Empty;

	[Option('p', "path", Default = "words.txt", HelpText = "Путь до источника слов.")]
	public string SourcePath { get; set; } = string.Empty;

	[Option('o', "output", Default = "output.png", HelpText = "Путь сохранения результата.")]
	public string SavePath { get; set; } = string.Empty;

	[Option("boringWords", HelpText = "Путь до списка скучный слов.")]
	public string BoringWordsPath { get; set; } = string.Empty;

	[Option("minFont", Default = 15, HelpText = "Минимальный шрифт.")]
	public int FontSizeMin { get; set; }

	[Option("maxFont", Default = 40, HelpText = "Максимальный шрифт.")]
	public int FontSizeMax { get; set; }

	public AppSettings GetAppSettings() =>
		new()
		{
			SourcePath = SourcePath,
			SavePath = SavePath,
			BoringWordsPath = BoringWordsPath
		};

	public ImageSettings GetImageSettings() =>
		new()
		{
			Width = Width,
			Height = Height,
			BackgroundColor = BackgroundColor,
			FontFamily = FontFamily,
			FontSizeMax = FontSizeMax,
			FontSizeMin = FontSizeMin
		};
}