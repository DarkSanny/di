using System.Collections.Generic;

namespace TagCloudBuilder.WordsConverter
{
	public interface IWordFilter
	{
		Result<IEnumerable<string>> FilterWords(IWordReader reader);
	}
}
