namespace GuiClient.Forms;

public static class ErrorExtensions
{
	public static void ShowError(string errorMessage)
	{
		MessageBox.Show($"В приложении произошла ошибка: {errorMessage}",
			"Ошибка",
			MessageBoxButtons.OK,
			MessageBoxIcon.Error);
	}
}