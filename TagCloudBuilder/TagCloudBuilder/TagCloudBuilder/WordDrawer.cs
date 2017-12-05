using System;
using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class WordDrawer : IWordDrawer
	{
		public  FontFamily FontFamily { get; set; }
		public FontStyle Style { get; set; }
		public  Brush Brush { get; set; }
		private readonly Func<int, int> _converterWeightToSize = (weight) => 20 + weight * 2;

		public WordDrawer()
		{
			FontFamily = new FontFamily("Arial");
			Style = new FontStyle();
			Brush = Brushes.Black;
		}


		public Size GetWordSize(Graphics graphics, WeightedWord weightedWord)
		{
			var font = new Font(FontFamily, _converterWeightToSize(weightedWord.Weight), Style);
			var measureString = graphics.MeasureString(weightedWord.Word, font);
			return measureString.ToSize();
		}

		public void DrawWord(Graphics graphics, WeightedWord weightedWord, Rectangle rectanglePlace)
		{
			graphics.DrawString(
				weightedWord.Word,
				new Font(FontFamily, _converterWeightToSize(weightedWord.Weight), Style),
				Brush,
				rectanglePlace.Location
			);
		}
	}
}