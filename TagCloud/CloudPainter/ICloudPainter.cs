using TagCloud.WordCounter;

namespace TagCloud.CloudPainter;

internal interface ICloudPainter
{
	void Paint(Tag[] tags);
}