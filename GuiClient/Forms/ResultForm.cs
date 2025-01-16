using Autofac;
using TagCloud;
using TagCloud.Common;
using TagCloud.Common.Extensions;
using TagCloud.Settings;

namespace GuiClient.Forms;

public class ResultForm : Form
{
	public ResultForm(Form mainForm, ILifetimeScope lifetimeScope, Settings settings)
	{
		InitForm(settings)
			.Then(_ => CreateRegenerateButton(mainForm))
			.Then(_ => lifetimeScope.Resolve<IApp>().Run(settings))
			.Then(_ => ShowPicture(settings))
			.OnFail(ErrorExtensions.ShowError);
	}

	private Result<None> InitForm(Settings settings)
	{
		try
		{
			Text = "Результат";
			Size = new Size(settings.ImageSettings.Width, settings.ImageSettings.Height + 70);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			StartPosition = FormStartPosition.CenterScreen;
			return Result.Ok();
		}
		catch (Exception e)
		{
			return Result.Fail<None>($"Во время инициализации формы возникли ошибки: {e.Message}");
		}
	}

	private Result<None> CreateRegenerateButton(Form mainForm)
	{
		try
		{
			var regenerateButton = new Button { Text = "Сгенерировать заново", Dock = DockStyle.Bottom, };
			regenerateButton.Click += (_, _) =>
			{
				mainForm.Show();
				Close();
			};
			Controls.Add(regenerateButton);
			return Result.Ok();
		}
		catch (Exception e)
		{
			return Result.Fail<None>($"Во время создания кнопки возникли ошибки: {e.Message} ");
		}
	}

	private Result<None> ShowPicture(Settings settings)
	{
		try
		{
			var picture = new PictureBox
			{
				SizeMode = PictureBoxSizeMode.AutoSize,
				ImageLocation = settings.AppSettings.SavePath,
				Dock = DockStyle.Top,
			};
			picture.Load();
			Controls.Add(picture);

			return Result.Ok();
		}
		catch (Exception e)
		{
			return Result.Fail<None>($"Во время загрузки изображения возникли ошибки: {e.Message}");
		}
	}
}