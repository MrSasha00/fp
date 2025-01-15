using Autofac;
using GuiClient.Forms;
using TagCloud;
using TagCloud.Client;

namespace GuiClient;

static class Program
{
	[STAThread]
	static void Main()
	{
		var builder = new ContainerBuilder();
		builder.RegisterModule(new TagCloudModule());
		builder.RegisterType<GuiClient>().As<IClient>();
		builder.RegisterType<MainForm>().InstancePerDependency();
		var container = builder.Build();

		var client = container.Resolve<IClient>();
		client.Run();
	}
}