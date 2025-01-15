using TagCloud.Settings;

namespace TagCloud;

public interface IApp
{
	void Run(AppSettings appSettings, ImageSettings imageSettings);
}