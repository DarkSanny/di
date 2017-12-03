using System.Drawing;

namespace TagCloudBuilder
{
	public interface IImageSaver
	{
		void SaveImage(Bitmap bitmap, string filepath);
	}
}
