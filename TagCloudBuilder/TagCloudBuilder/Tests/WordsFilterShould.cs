using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.Tests
{
	[TestFixture]
	public class WordsFilterShould
	{
		private List<string> _words;
		private Mock<IWordReader> _reader;
		private Mock<IWordAnalyzer> _analyzer;
		private Mock<IWordConverter> _converter;

		[SetUp]
		public void SetUp()
		{
			_words = new List<string>() {"слово"};
			_reader = new Mock<IWordReader>();
			_reader.Setup((r) => r.ReadWords()).Returns(_words);
			_analyzer = new Mock<IWordAnalyzer>();
			_analyzer.Setup((a) => a.IsCorrectWord(It.IsAny<string>())).Returns(true);
			_converter = new Mock<IWordConverter>();
			_converter.Setup((c) => c.ConvertWord(It.IsAny<string>())).Returns((string s) => s);
		}

		[Test]
		public void ReturnSameWords_WhenAnalyzerAndConverterDoingNothing()
		{
			var filter = new WordsFilter(_analyzer.Object, _converter.Object);
			filter.FilterWords(_reader.Object).ShouldBeEquivalentTo(_words);
		}

		[Test]
		public void ExcludeIncorrectWord()
		{
			var analyzer = new Mock<IWordAnalyzer>();
			analyzer.Setup((a) => a.IsCorrectWord("слово")).Returns(false);
			var filter = new WordsFilter(analyzer.Object, _converter.Object);
			filter.FilterWords(_reader.Object).Should().BeEmpty();
		}

		[Test]
		public void ChangeWords()
		{
			var converter = new Mock<IWordConverter>();
			converter.Setup((c) => c.ConvertWord(It.IsAny<string>())).Returns((string s) => s + "!");
			var filter = new WordsFilter(_analyzer.Object, converter.Object);
			filter.FilterWords(_reader.Object).ShouldBeEquivalentTo(new List<string>() {"слово!"});
		}

		[Test]
		public void ReturnInLowerCase_WhenWordConverter()
		{
			var filter = new WordsFilter(_analyzer.Object, new WordConverter());
			filter.FilterWords(_reader.Object).ShouldBeEquivalentTo(_words.Select(w => w.ToLower()));
		}

		[Test]
		public void ExcludeWords_WhenBoringWords()
		{
			var words = new List<string>() {"и"};
			var reader = new Mock<IWordReader>();
			reader.Setup((r) => r.ReadWords()).Returns(words);
			var filter = new WordsFilter(new BoringWordsAnalyzer(), _converter.Object);
			filter.FilterWords(reader.Object).Should().BeEmpty();
		}

		[Test]
		public void Reader_ShouldBePerformedOnce()
		{
			var calls = 0;
			var reader = new Mock<IWordReader>();
			reader.Setup((r) => r.ReadWords()).Returns(_words)
				.Callback(() => calls++);
			var filter = new WordsFilter(_analyzer.Object, _converter.Object);
			filter.FilterWords(reader.Object);
			calls.Should().Be(1);
		}

		[Test]
		public void Analyzer_ShoultCheckEachWords()
		{
			var words = new List<string>() {"первое", "второе"};
			var reader = new Mock<IWordReader>();
			reader.Setup((r) => r.ReadWords()).Returns(words);
			var analyzer = new Mock<IWordAnalyzer>();
			var calls = 0;
			analyzer.Setup((a) => a.IsCorrectWord(It.IsAny<string>())).Returns(true).Callback(() => calls++);
			var filter = new WordsFilter(analyzer.Object, _converter.Object);
			filter.FilterWords(reader.Object).ToList();
			calls.Should().Be(words.Count);
		}
	}
}