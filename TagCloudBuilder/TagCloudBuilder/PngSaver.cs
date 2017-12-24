using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloudBuilder
{
	public class PngSaver :IImageSaver
	{
		public Result<None> SaveImage(Bitmap bitmap, string filepath)
		{
			return Result.OfAction(() => bitmap.Save(filepath, ImageFormat.Png));
		}
	}
}
