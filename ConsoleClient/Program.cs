using Autofac;
using TagCloud;
using TagCloud.Client;

namespace ConsoleClient;

class Program
{
	static void Main()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule(new TagCloudModule());
		builder.RegisterType<ConsoleClient>().As<IClient>();
		var container = builder.Build();
		var app = container.Resolve<IClient>();

		app.Run();
	}
}