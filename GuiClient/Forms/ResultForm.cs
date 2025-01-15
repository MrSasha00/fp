using Autofac;
using TagCloud;
using TagCloud.Settings;

namespace GuiClient.Forms;

public class ResultForm : Form
{
	private readonly ILifetimeScope _lifetimeScope;

	public ResultForm(Form mainForm, ILifetimeScope lifetimeScope, AppSettings appSettings, ImageSettings imageSettings)
	{
		_lifetimeScope = lifetimeScope;

		using var scope = _lifetimeScope.BeginLifetimeScope();

		Text = "Результат";
		Size = new Size(imageSettings.Width, imageSettings.Height + 70);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		StartPosition = FormStartPosition.CenterScreen;

		var regenerateButton = new Button { Text = "Сгенерировать заново", Dock = DockStyle.Bottom, };
		regenerateButton.Click += (_, _) =>
		{
			mainForm.Show();
			Close();
		};

		ShowCloudImage(appSettings, imageSettings);
		Controls.Add(regenerateButton);
	}

	private void ShowCloudImage(AppSettings appSettings, ImageSettings imageSettings)
	{
		_lifetimeScope.Resolve<IApp>().Run(appSettings, imageSettings);
		var picture = new PictureBox
		{
			SizeMode = PictureBoxSizeMode.AutoSize,
			ImageLocation = appSettings.SavePath,
			Dock = DockStyle.Top,
		};
		picture.Load();
		Controls.Add(picture);
	}
}