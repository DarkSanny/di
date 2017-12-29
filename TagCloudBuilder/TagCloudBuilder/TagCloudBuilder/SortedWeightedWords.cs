using System.Collections.Generic;
using System.Linq;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.TagCloudBuilder
{
	public class SortedWeightedWords : IWordWeighter
	{
		private readonly IWordFilter _filter;

		public SortedWeightedWords(IWordFilter filter)
		{
			_filter = filter;
		}

		public Result<IEnumerable<WeightedWord>> WeightWords(IWordReader reader)
		{
			return _filter
				.FilterWords(reader)
				.Then(words => words.GroupBy(word => word))
				.Then(groups => groups.Select(group => new WeightedWord(group.Key, group.Count())))
				.Then(weightedWords => (IEnumerable<WeightedWord>) weightedWords.OrderByDescending(ww => ww.Weight));
		}
	}
}
