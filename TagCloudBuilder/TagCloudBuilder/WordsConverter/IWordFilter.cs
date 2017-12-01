using System.Collections.Generic;

namespace TagCloudBuilder.WordsConverter
{
	public interface IWordFilter
	{

		List<string> FilterWords(IWordReader reader);

	}
}
