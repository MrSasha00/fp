using Autofac;
using GuiClient.Forms;
using TagCloud.Client;

namespace GuiClient;

public class GuiClient(ILifetimeScope lifetimeScope) : IClient
{
	public void Run()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);

		var mainForm = lifetimeScope.Resolve<MainForm>();
		Application.Run(mainForm);
	}
}