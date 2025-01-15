using CommandLine;
using ConsoleClient.Settings;
using TagCloud;
using TagCloud.Client;

namespace ConsoleClient;

public class ConsoleClient(IApp app) : IClient
{
	public void Run()
	{
		try
		{
			Parser.Default.ParseArguments<ConsoleSettings>(Environment.GetCommandLineArgs())
				.WithParsed(settings => app.Run(new TagCloud.Settings.Settings
				{
					AppSettings = settings.GetAppSettings(),
					ImageSettings = settings.GetImageSettings()
				}))
				.WithNotParsed(_ => throw new ArgumentException("Invalid command line arguments"));
		}
		catch (Exception e)
		{
			Console.WriteLine($"Во время выполнения программы возникла ошибка: {e.Message}");
		}
	}
}