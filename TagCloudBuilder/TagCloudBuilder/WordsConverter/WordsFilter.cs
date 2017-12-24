using System.Collections.Generic;
using System.Linq;

namespace TagCloudBuilder.WordsConverter
{
	public class WordsFilter : IWordFilter
	{
		private readonly IWordAnalyzer _analyzer;
		private readonly IWordConverter _converter;

		public WordsFilter(IWordAnalyzer analyzer, IWordConverter converter)
		{
			_analyzer = analyzer;
			_converter = converter;
		}

		public Result<IEnumerable<string>> FilterWords(IWordReader reader)
		{
			return reader.ReadWords()
				.Then(words => words.Select(_converter.ConvertWord))
				.Then(words => words.Where(_analyzer.IsCorrectWord));
		}
	}
}