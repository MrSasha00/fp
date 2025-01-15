using System.Drawing;

namespace TagCloud.CloudPainter;

internal interface IImageSaver
{
	void SaveImage(Bitmap image, string fullFileName);
}