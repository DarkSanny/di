using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public interface ICloudBuilder
	{
		Result<Rectangle> PutNextRectangle(Size rectangleSize);
	}
}
