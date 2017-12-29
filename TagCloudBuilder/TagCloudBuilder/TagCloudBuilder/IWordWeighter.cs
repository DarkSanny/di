using System.Collections.Generic;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.TagCloudBuilder
{
	public interface IWordWeighter
	{
		Result<IEnumerable<WeightedWord>> WeightWords(IWordReader reader);
	}
}
