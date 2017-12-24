using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public interface IImageBuilder
	{
		Result<Bitmap> BuildImage();
	}
}
