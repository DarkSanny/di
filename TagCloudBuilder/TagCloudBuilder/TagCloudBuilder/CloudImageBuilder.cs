using System.Drawing;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class CloudImageBuilder :IImageBuilder
	{
		private readonly IWordWeighter _weighter;
		private readonly IWordDrawer _drawer;
		private Bitmap _cloudImage;

		public CloudImageBuilder(
			ICloudBuilder builder,
			IWordReader reader,
			IWordWeighter weighter,
			IWordDrawer drawer,
			Size imageSize
			)
		{
			_weighter = weighter;
			_drawer = drawer;
			BuildCloudImage(builder, reader, imageSize);
		}

		public void RebuildCloudImage(ICloudBuilder builder, IWordReader reader, Size imageSize)
		{
			BuildCloudImage(builder, reader, imageSize);
		}

		private void BuildCloudImage(ICloudBuilder builder, IWordReader reader, Size imageSize)
		{
			_cloudImage = new Bitmap(imageSize.Width, imageSize.Height);
			var graphics = Graphics.FromImage(_cloudImage);
			var weightedWords = _weighter.WeightWords(reader);
			foreach (var weightedWord in weightedWords)
			{
				var rectanglePlace = builder.PutNextRectangle(_drawer.GetWordSize(graphics, weightedWord));
				graphics.DrawRectangle(Pens.RoyalBlue, rectanglePlace);
				_drawer.DrawWord(graphics, weightedWord, rectanglePlace);
			}
		}

		public Bitmap BuildImage()
		{
			return _cloudImage;
		}
	}
}
