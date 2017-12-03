using System.Collections.Generic;

namespace TagCloudBuilder.WordsConverter
{
	public interface IWordReader
	{
		List<string> ReadWords();
	}
}
