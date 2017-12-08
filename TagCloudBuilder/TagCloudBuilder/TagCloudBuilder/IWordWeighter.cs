using System.Collections.Generic;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.TagCloudBuilder
{
	public interface IWordWeighter
	{
		IEnumerable<WeightedWord> WeightWords(IWordReader reader);
	}
}
