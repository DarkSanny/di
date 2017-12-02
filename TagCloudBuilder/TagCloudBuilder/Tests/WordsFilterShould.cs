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
		private Mock<IWordAnalizator> _analizator;
		private Mock<IWordConverter> _converter;

		[SetUp]
		public void SetUp()
		{
			_words = new List<string>() {"слово"};
			_reader = new Mock<IWordReader>();
			_reader.Setup((r) => r.ReadWords()).Returns(_words);
			_analizator = new Mock<IWordAnalizator>();
			_analizator.Setup((a) => a.IsCorrectWord(It.IsAny<string>())).Returns(true);
			_converter = new Mock<IWordConverter>();
			_converter.Setup((c) => c.ConvertWord(It.IsAny<string>())).Returns((string s) => s);
		}

		[Test]
		public void ReturnSameWords_WhenAnalizatorAndConverterDoingNothing()
		{
			var filter = new WordsFilter(_analizator.Object, _converter.Object);
			filter.FilterWords(_reader.Object).ShouldBeEquivalentTo(_words);
		}

		[Test]
		public void ExludeIncorrectWord()
		{
			var analizator = new Mock<IWordAnalizator>();
			analizator.Setup((a) => a.IsCorrectWord("слово")).Returns(false);
			var filter = new WordsFilter(analizator.Object, _converter.Object);
			filter.FilterWords(_reader.Object).Should().BeEmpty();
		}

		[Test]
		public void ChangeWords()
		{
			var converter = new Mock<IWordConverter>();
			converter.Setup((c) => c.ConvertWord(It.IsAny<string>())).Returns((string s) => s + "!");
			var filter = new WordsFilter(_analizator.Object, converter.Object);
			filter.FilterWords(_reader.Object).ShouldBeEquivalentTo(new List<string>() {"слово!"});
		}

		[Test]
		public void ReturnInLoverCase_WhenWordConverter()
		{
			var filter = new WordsFilter(_analizator.Object, new WordConverter());
			filter.FilterWords(_reader.Object).ShouldBeEquivalentTo(_words.Select(w => w.ToLower()));
		}

		[Test]
		public void ExludeWords_WhenBoringWords()
		{
			var words = new List<string>() {"и"};
			var reader = new Mock<IWordReader>();
			reader.Setup((r) => r.ReadWords()).Returns(words);
			var filter = new WordsFilter(new BoringWordsAnalizator(), _converter.Object);
			filter.FilterWords(reader.Object).Should().BeEmpty();
		}
	}
}