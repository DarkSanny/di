using System.Drawing;

namespace TagCloudBuilder.TagCloudBuilder
{
	public interface IWordDrawer
	{
		Result<Size> GetWordSize(Graphics graphics, WeightedWord weightedWord);
		void DrawWord(Graphics graphics, WeightedWord weightedWord, Rectangle rectanglePlace);
	}
}
