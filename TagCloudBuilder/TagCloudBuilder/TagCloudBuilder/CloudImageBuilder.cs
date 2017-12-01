using System.Drawing;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class CloudImageBuilder :IImageBuilder
	{
		private readonly ICloudBuilder _builder;
		private readonly IWordReader _reader;
		private readonly IWordWeighter _weighter;
		private readonly IWordDrawer _drawer;
		private readonly Size _imageSize;
		private Bitmap _cloudImage;

		public CloudImageBuilder(
			ICloudBuilder builder,
			IWordReader reader,
			IWordWeighter weighter,
			IWordDrawer	drawer,
			Size imageSize
			)
		{
			_builder = builder;
			_reader = reader;
			_weighter = weighter;
			_drawer = drawer;
			_imageSize = imageSize;
			BuildCloudImage();
		}

		private void BuildCloudImage()
		{
			_cloudImage = new Bitmap(_imageSize.Width, _imageSize.Height);
			var graphics = Graphics.FromImage(_cloudImage);
			var weightedWords = _weighter.WeightWords(_reader);
			foreach (var weightedWord in weightedWords)
			{
				var rectanglePlace = _builder.PutNextRectangle(_drawer.GetWordSize(graphics, weightedWord));
				_drawer.DrawWord(graphics, weightedWord, rectanglePlace);
			}
		}

		public Bitmap BuildImage()
		{
			return _cloudImage;
		}
	}
}
