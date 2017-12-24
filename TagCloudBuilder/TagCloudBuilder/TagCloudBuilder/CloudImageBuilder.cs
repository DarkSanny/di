using System;
using System.Drawing;
using System.Linq;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class CloudImageBuilder :IImageBuilder
	{
		private readonly IWordWeighter _weighter;
		private readonly IWordDrawer _drawer;
		private Result<Bitmap> _cloudImage;

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
			_cloudImage = Result.Of(() => new Bitmap(imageSize.Width, imageSize.Height))
				.Then(image => DrawCloud(builder, reader, imageSize, image));	
		}

		private Bitmap DrawCloud(ICloudBuilder builder, IWordReader reader, Size imageSize, Bitmap bitmap)
		{
			var graphics = Graphics.FromImage(bitmap);
			_weighter.WeightWords(reader)
				.ToList()
				.ForEach(weightedWord => builder.PutNextRectangle(_drawer.GetWordSize(graphics, weightedWord).Value)
				.Then((rectangle) => DrawWord(graphics, imageSize, rectangle, weightedWord)));
			return bitmap;
		}

		private void DrawWord(Graphics graphics, Size imageSize,  Rectangle rectanglePlace, WeightedWord weightedWord)
		{
			if (IsRectangleOutOfSize(rectanglePlace, imageSize))
				throw new ArgumentException("Rectangle out of image");
			graphics.DrawRectangle(Pens.RoyalBlue, rectanglePlace);
			_drawer.DrawWord(graphics, weightedWord, rectanglePlace);
		}

		private static bool IsRectangleOutOfSize(Rectangle rectangle, Size size) => 
			rectangle.X < 0 
			|| rectangle.X + rectangle.Width > size.Width 
			|| rectangle.Y < 0 
			||rectangle.Y + rectangle.Height > size.Height;

		public Result<Bitmap> BuildImage()
		{
			return _cloudImage;
		}
	}
}
