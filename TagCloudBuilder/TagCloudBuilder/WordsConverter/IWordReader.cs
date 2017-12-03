using System.Collections.Generic;

namespace TagCloudBuilder.WordsConverter
{
	public interface IWordReader
	{
		IEnumerable<string> ReadWords();
	}
}
