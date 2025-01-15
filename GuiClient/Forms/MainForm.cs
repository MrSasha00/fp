using Autofac;
using TagCloud.Settings;

namespace GuiClient.Forms;

public class MainForm : Form
{
	private readonly ILifetimeScope _lifetimeScope;

	private NumericUpDown? _widthInput;
	private NumericUpDown? _heightInput;
	private Button? _colorButton;
	private TextBox? _sourceFilePathInput;
	private TextBox? _boringWordsPathFileInput;
	private Button? _browseSourceFileButton;
	private Button? _browseBoringWordsPathFileButton;
	private Button? _fontButton;
	private NumericUpDown? _minFontSizeInput;
	private NumericUpDown? _maxFontSizeInput;
	private Button? _generateButton;
	private Color _selectedColor = Color.White;
	private Font _selectedFont = new("Arial", 10);

	private const int LabelX = 20;
	private const int InputX = 130;
	private string _sourceFilePath = string.Empty;
	private string _boringWordsFilePath = string.Empty;

	public MainForm(ILifetimeScope lifetimeScope)
	{
		_lifetimeScope = lifetimeScope;
		InitForm();
		AddWidthHeightControl();
		AddColorControl();
		AddSourceFilePathSelector();
		AddBoringWordsFilePathSelector();
		AddFontSelector();
		AddMinMaxFontSizeControls();
		AddGenerateButton();
	}

	private void InitForm()
	{
		Text = "Tag Cloud";
		Size = new Size(480, 420);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		StartPosition = FormStartPosition.CenterScreen;
	}

	private void AddWidthHeightControl()
	{
		var widthLabel = new Label { Text = "Ширина:", Location = new Point(LabelX, 20), AutoSize = true };
		_widthInput = new NumericUpDown
			{ Location = new Point(InputX, 20), Minimum = 100, Maximum = 1920, Value = 400 };

		var heightLabel = new Label { Text = "Высота:", Location = new Point(LabelX, 60), AutoSize = true };
		_heightInput = new NumericUpDown
			{ Location = new Point(InputX, 60), Minimum = 100, Maximum = 1080, Value = 300 };

		Controls.Add(widthLabel);
		Controls.Add(_widthInput);
		Controls.Add(heightLabel);
		Controls.Add(_heightInput);
	}

	private void AddColorControl()
	{
		var colorLabel = new Label { Text = "Цвет:", Location = new Point(LabelX, 100), AutoSize = true };
		_colorButton = new Button { Text = "Выбрать цвет", Location = new Point(250, 100) };
		var colorPicture = new PictureBox
		{
			Location = new Point(InputX, 100),
			Width = 100,
			Height = 20,
			BackColor = _selectedColor
		};

		_colorButton.Click += (_, _) =>
		{
			using var colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				_selectedColor = colorDialog.Color;
				colorPicture.BackColor = colorDialog.Color;
			}
		};

		Controls.Add(colorLabel);
		Controls.Add(_colorButton);
		Controls.Add(colorPicture);
	}

	private void AddSourceFilePathSelector()
	{
		var sourceFilePathLabel = new Label { Text = "Источник:", Location = new Point(LabelX, 140), AutoSize = true };
		_sourceFilePathInput = new TextBox { Location = new Point(InputX, 140), Width = 200 };
		_browseSourceFileButton = new Button { Text = "Обзор...", Location = new Point(360, 140) };
		_browseSourceFileButton.Click += (_, _) =>
		{
			using var openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text Files (*.txt)|*.txt;";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_sourceFilePathInput.Text = openFileDialog.FileName;
				_sourceFilePath = openFileDialog.FileName;
			}
		};

		Controls.Add(sourceFilePathLabel);
		Controls.Add(_sourceFilePathInput);
		Controls.Add(_browseSourceFileButton);
	}

	private void AddBoringWordsFilePathSelector()
	{
		var additionalFilePathLabel = new Label
			{ Text = "Скучные слова:", Location = new Point(LabelX, 180), AutoSize = true };
		_boringWordsPathFileInput = new TextBox { Location = new Point(InputX, 180), Width = 200 };
		_browseBoringWordsPathFileButton = new Button { Text = "Обзор...", Location = new Point(360, 180) };
		_browseBoringWordsPathFileButton.Click += (_, _) =>
		{
			using var openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text Files (*.txt)|*.txt";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				_boringWordsPathFileInput.Text = openFileDialog.FileName;
				_boringWordsFilePath = openFileDialog.FileName;
			}
		};
		Controls.Add(additionalFilePathLabel);
		Controls.Add(_boringWordsPathFileInput);
		Controls.Add(_browseBoringWordsPathFileButton);
	}

	private void AddFontSelector()
	{
		var fontLabel = new Label { Text = "Шрифт:", Location = new Point(LabelX, 220), AutoSize = true };
		_fontButton = new Button { Text = "Выбрать шрифт", Location = new Point(InputX, 220) };
		_fontButton.Click += (_, _) =>
		{
			using var fontDialog = new FontDialog();
			if (fontDialog.ShowDialog() == DialogResult.OK)
			{
				_selectedFont = fontDialog.Font;
			}
		};

		Controls.Add(fontLabel);
		Controls.Add(_fontButton);
	}

	private void AddMinMaxFontSizeControls()
	{
		var minFontSizeLabel = new Label { Text = "Мин. шрифт:", Location = new Point(LabelX, 260), AutoSize = true };
		_minFontSizeInput = new NumericUpDown
			{ Location = new Point(InputX, 260), Minimum = 8, Maximum = 72, Value = 8 };

		var maxFontSizeLabel = new Label { Text = "Макс. шрифт:", Location = new Point(LabelX, 300), AutoSize = true };
		_maxFontSizeInput = new NumericUpDown
			{ Location = new Point(InputX, 300), Minimum = 8, Maximum = 72, Value = 24 };

		Controls.Add(minFontSizeLabel);
		Controls.Add(_minFontSizeInput);
		Controls.Add(maxFontSizeLabel);
		Controls.Add(_maxFontSizeInput);
	}

	private void AddGenerateButton()
	{
		_generateButton = new Button { Text = "Сгенерировать", Location = new Point(200, 340), AutoSize = true };
		_generateButton.Click += GenerateButton_Click!;

		Controls.Add(_generateButton);
	}

	private void GenerateButton_Click(object sender, EventArgs e)
	{
		var imageSettings = new ImageSettings
		{
			Width = (int)_widthInput!.Value,
			Height = (int)_heightInput!.Value,
			BackgroundColor = _selectedColor.Name,
			FontFamily = _selectedFont.FontFamily.Name,
			FontSizeMax = (int)_minFontSizeInput!.Value,
			FontSizeMin = (int)_maxFontSizeInput!.Value
		};

		var appSettings = new AppSettings
		{
			SourcePath = _sourceFilePath,
			BoringWordsPath = _boringWordsFilePath,
			SavePath = "output.png"
		};

		var resultForm = new ResultForm(this, _lifetimeScope, appSettings, imageSettings);
		resultForm.Show();
		Hide();
	}
}