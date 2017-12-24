using System.Collections.Generic;

namespace TagCloudBuilder.WordsConverter
{
	public interface IWordReader
	{
		Result<IEnumerable<string>> ReadWords();
	}
}
