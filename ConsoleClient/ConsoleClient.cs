using CommandLine;
using ConsoleClient.Settings;
using TagCloud;
using TagCloud.Client;
using TagCloud.Common;
using TagCloud.Common.Extensions;

namespace ConsoleClient;

public class ConsoleClient(IApp app) : IClient
{
	public void Run()
	{
		Result.Of(() => Parser.Default.ParseArguments<ConsoleSettings>(Environment.GetCommandLineArgs()))
			.Then(result => HandleParsedArguments(result))
			.OnFail(error => Console.WriteLine($"Во время выполнения программы возникла ошибка: {error}"));
	}

	private Result<None> HandleParsedArguments(ParserResult<ConsoleSettings> parsedArgs) =>
		parsedArgs.MapResult(
			RunApplication,
			_ => Result.Fail<None>("Invalid command line arguments")
		);

	private Result<None> RunApplication(ConsoleSettings settings) =>
		app.Run(new TagCloud.Settings.Settings
		{
			AppSettings = settings.GetAppSettings(),
			ImageSettings = settings.GetImageSettings()
		});
}