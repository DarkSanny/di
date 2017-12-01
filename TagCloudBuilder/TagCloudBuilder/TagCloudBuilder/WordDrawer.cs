using System;
using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class WordDrawer : IWordDrawer
	{
		private readonly FontFamily _fontFamily;
		private readonly FontStyle _style;
		private readonly Brush _brush;
		private readonly Func<int, int> _converterWeightToSize = (weight) => 20 + weight * 2;

		public WordDrawer(FontFamily fontFamily, FontStyle style,  Brush brush)
		{
			_fontFamily = fontFamily;
			_style = style;
			_brush = brush;
		}

		public Size GetWordSize(Graphics graphics, WeightedWord weightedWord)
		{
			var font = new Font(_fontFamily, _converterWeightToSize(weightedWord.Weight), _style);
			var measureString = graphics.MeasureString(weightedWord.Word, font);
			return measureString.ToSize();
		}

		public void DrawWord(Graphics graphics, WeightedWord weightedWord, Rectangle rectanglePlace)
		{
			graphics.DrawString(
				weightedWord.Word,
				new Font(_fontFamily, _converterWeightToSize(weightedWord.Weight), _style),
				_brush,
				rectanglePlace,
				new StringFormat
				{
					Alignment = StringAlignment.Center,
					LineAlignment = StringAlignment.Center,
					FormatFlags = StringFormatFlags.FitBlackBox
				}
			);
		}
	}
}