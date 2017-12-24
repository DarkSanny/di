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

		public IEnumerable<WeightedWord> WeightWords(IWordReader reader)
		{
			return _filter
				.FilterWords(reader)
				.Then(words => words.GroupBy(word => word)
					.Select(group => new WeightedWord(group.Key, group.Count()))
					.OrderByDescending(weightedWord => weightedWord.Weight)
					.ToList()).GetValueOrThrow();

		}
	}
}
