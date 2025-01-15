using TagCloud.Common;
using TagCloud.WordCounter;

namespace TagCloud.CloudPainter;

internal interface ICloudPainter
{
	Result<None> Paint(IEnumerable<Tag> tags);
}