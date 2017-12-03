using System.Collections.Generic;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.TagCloudBuilder
{
	public interface IWordWeighter
	{
		List<WeightedWord> WeightWords(IWordReader reader);
	}
}
