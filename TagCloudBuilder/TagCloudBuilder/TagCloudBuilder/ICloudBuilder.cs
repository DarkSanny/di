using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public interface ICloudBuilder
	{
		Rectangle PutNextRectangle(Size rectangleSize);
	}
}
