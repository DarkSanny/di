using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudBuilder
{
	public class PngSaver	:IImageSaver
	{
		private readonly string _filepath;

		public PngSaver(string filepath)
		{
			_filepath = filepath;
		}

		public void SaveImage(Bitmap bitmap)
		{
			bitmap.Save(_filepath, ImageFormat.Png);
		}
	}
}
