using System.Collections.Generic;

namespace TagCloudBuilder.WordsConverter
{
	public interface IWordFilter
	{
		IEnumerable<string> FilterWords(IWordReader reader);
	}
}
