using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class CloudSettings
	{
		public string PathToWords { get; set; }
		public string PathToImage { get; set; }
		public string ImageName { get; set; }
		public Size ImageSize { get; set; }

		public CloudSettings(string pathToWords, string pathToImage, string imageName, Size imageSize)
		{
			PathToWords = pathToWords;
			PathToImage = pathToImage;
			ImageName = imageName;
			ImageSize = imageSize;
		}

		public CloudSettings()
		{
		}
	}
}
