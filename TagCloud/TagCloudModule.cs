using Autofac;
using TagCloud.CloudPainter;
using TagCloud.Settings;
using TagCloud.TagPositioner;
using TagCloud.TagPositioner.Circular;
using TagCloud.WordCounter;
using TagCloud.WordsProcessing;
using TagCloud.WordsReader;

namespace TagCloud;

public class TagCloudModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		builder.RegisterType<App>().As<IApp>();
		builder.RegisterType<TxtWordsReader>().As<IWordsReader>();
		builder.RegisterType<WordPreprocessor>().As<IWordPreprocessor>();
		builder.RegisterType<TagCreator>().As<ITagCreator>();
		builder.RegisterType<FIleBoringWordsProvider>().As<IBoringWordsProvider>();
		builder.RegisterType<AppSettingsProvider>().As<IAppSettingsProvider>().SingleInstance();
		builder.RegisterType<ImageSettingsProvider>().As<IImageSettingsProvider>().SingleInstance();
		builder.RegisterType<CloudPainter.CloudPainter>().As<ICloudPainter>();
		builder.RegisterType<PngImageSaver>().As<IImageSaver>();
		builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();
		builder.RegisterType<TagPositioner.TagPositioner>().As<ITagPositioner>();
	}
}