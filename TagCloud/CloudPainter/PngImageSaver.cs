using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.CloudPainter;

internal class PngImageSaver : IImageSaver
{
	public void SaveImage(Bitmap image, string fullFileName)
	{
		image.Save(fullFileName, ImageFormat.Png);
		image.Dispose();
	}
}