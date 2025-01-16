using TagCloud.Common;

namespace TagCloud;

public interface IApp
{
	Result<None> Run(Settings.Settings settings);
}