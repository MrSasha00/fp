namespace TagCloud.Settings;

public interface IAppSettingsProvider
{
	AppSettings AppSettings { get; set; }
}