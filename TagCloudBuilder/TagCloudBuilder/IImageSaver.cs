using System.Drawing;

namespace TagCloudBuilder
{
	public interface IImageSaver
	{
		Result<None> SaveImage(Bitmap bitmap, string filepath);
	}
}
