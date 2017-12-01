using System.Collections.Generic;
using System.Linq;

namespace TagCloudBuilder.WordsConverter
{
	public class WordsFilter : IWordFilter
	{
		private readonly IWordAnalizator _analizator;
		private readonly IWordConverter _converter;

		public WordsFilter(IWordAnalizator analizator, IWordConverter converter)
		{
			_analizator = analizator;
			_converter = converter;
		}

		public List<string> FilterWords(IWordReader reader)
		{
			return reader
				.ReadWords()
				.Select(_converter.ConvertWord)
				.Where(_analizator.IsCorrectWord)
				.ToList();
		}
	}
}