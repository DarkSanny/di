using System;
using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class WordDrawer : IWordDrawer
	{
		private FontFamily _fontFamily;
		private FontStyle _style;
		private Brush _brush;
		private readonly Func<int, int> _converterWeightToSize = (weight) => 20 + weight * 2;

		public WordDrawer()
		{
			_fontFamily = new FontFamily("Arial");
			_style = new FontStyle();
			_brush = Brushes.Black;
		}

		public Result<Size> GetWordSize(Graphics graphics, WeightedWord weightedWord)
		{
			return Result.Of(() =>
			{
				var font = new Font(_fontFamily, _converterWeightToSize(weightedWord.Weight), _style);
				var measureString = graphics.MeasureString(weightedWord.Word, font);
				return measureString.ToSize();
			});
		}

		public void DrawWord(Graphics graphics, WeightedWord weightedWord, Rectangle rectanglePlace)
		{
			graphics.DrawString(
				weightedWord.Word,
				new Font(_fontFamily, _converterWeightToSize(weightedWord.Weight), _style),
				_brush,
				rectanglePlace.Location
			);
		}

		public void SetFontFamily(FontFamily fontFamily)
		{
			_fontFamily = fontFamily;
		}

		public void SetFontStyle(FontStyle style)
		{
			_style = style;
		}

		public void SetBrush(Brush brush)
		{
			_brush = brush;
		}
	}
}