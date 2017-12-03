using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudBuilder
{
	public class PngSaver :IImageSaver
	{
		public void SaveImage(Bitmap bitmap, string filepath)
		{
			bitmap.Save(filepath, ImageFormat.Png);
		}
	}
}
