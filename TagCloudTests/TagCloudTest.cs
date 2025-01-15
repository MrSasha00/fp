using Autofac;
using TagCloud;
using TagCloud.Settings;

namespace TagCloudTests;

[TestFixture]
public class TagCloudTest
{
	private IContainer _container;

	[SetUp]
	public void SetUp()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule(new TagCloudModule());
		_container = builder.Build();
	}

	[TearDown]
	public void TearDown() =>
		_container.Dispose();

	[Test]
	public void Test()
	{
		var imageSettings = new ImageSettings
		{
			Width = 500,
			Height = 500,
			BackgroundColor = "red",
			FontFamily = "Arial",
			FontSizeMax = 40,
			FontSizeMin = 10
		};
		var appSettings = new AppSettings
		{
			SourcePath = TestConstants.SamplesWordsTestFile,
			SavePath = @"..\\..\..\test.png",
			BoringWordsPath = TestConstants.BoringWordsTestFile
		};

		var app = _container.Resolve<IApp>();
		app.Run(appSettings, imageSettings);
	}
}