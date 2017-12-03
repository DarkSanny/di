using System.Drawing;

namespace TagCloudBuilder
{
	public static class BitmapExtensions
	{
		public static void SaveImage(this Bitmap bitmap, IImageSaver saver)
		{
			saver.SaveImage(bitmap);
		}
	}
}
