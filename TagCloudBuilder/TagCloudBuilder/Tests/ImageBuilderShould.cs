using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagCloudBuilder.TagCloudBuilder;
using TagCloudBuilder.WordsConverter;

namespace TagCloudBuilder.Tests
{
	[TestFixture]
	public class ImageBuilderShould
	{
		private Mock<ICloudBuilder> _cloudBuilder;
		private Mock<IWordReader> _wordReader;
		private Mock<IWordWeighter> _wordWeighter;
		private Mock<IWordDrawer> _wordDrawer;
		private readonly Size _defaultSize = new Size(100, 100);
		private readonly List<string> _defaultWords = new List<string>() { "слово" };

		[SetUp]
		public void SetUp()
		{
			_cloudBuilder = new Mock<ICloudBuilder>();
			_wordReader = new Mock<IWordReader>();
			_wordWeighter = new Mock<IWordWeighter>();
			_wordWeighter.Setup((w) => w.WeightWords(It.IsAny<IWordReader>()))
				.Returns((IWordReader r) => r.ReadWords().Then(words => words.Select(w => new WeightedWord(w, 1))));
			_wordDrawer = new Mock<IWordDrawer>();
		}


		[Test]
		public void Result_ShouldHaveSameSize()
		{
			var imageBuilder =
				new CloudImageBuilder(_cloudBuilder.Object, _wordReader.Object, _wordWeighter.Object, _wordDrawer.Object, _defaultSize);
			var result = imageBuilder.BuildImage();
			result.Value.Size.Should().Be(_defaultSize);
		}

		[Test]
		public void PutNextRectangle_ShouldBePerformedOnce_WhenOneWord()
		{
			var calls = 0;
			var cloudBuilder = new Mock<ICloudBuilder>();
			_wordReader.Setup((r) => r.ReadWords()).Returns(_defaultWords);
			cloudBuilder.Setup((b) => b.PutNextRectangle(It.IsAny<Size>()))
				.Returns((Size s) => new Rectangle(-s.Width / 2, -s.Height / 2, s.Width, s.Height))
				.Callback(() => calls++);
			new CloudImageBuilder(cloudBuilder.Object, _wordReader.Object, _wordWeighter.Object, _wordDrawer.Object, _defaultSize);
			calls.Should().Be(1);
		}

		[Test]
		public void PutNextRectangle_ShouldBePerformedTwice_WhenTwoWords()
		{
			var words = new List<string>() {"первое", "второе"};
			_wordReader.Setup((r) => r.ReadWords()).Returns(words);
			var calls = 0;
			var cloudBuilder = new Mock<ICloudBuilder>();
			cloudBuilder.Setup((b) => b.PutNextRectangle(It.IsAny<Size>()))
				.Returns((Size s) => new Rectangle(-s.Width / 2, -s.Height / 2, s.Width, s.Height))
				.Callback(() => calls++);
			new CloudImageBuilder(cloudBuilder.Object, _wordReader.Object, _wordWeighter.Object, _wordDrawer.Object, _defaultSize);
			calls.Should().Be(2);
		}

		[Test]
		public void Rebuild_ShouldBuildNewResult()
		{
			var imageBuilder =
				new CloudImageBuilder(_cloudBuilder.Object, _wordReader.Object, _wordWeighter.Object, _wordDrawer.Object, _defaultSize);
			var result1 = imageBuilder.BuildImage();
			imageBuilder.RebuildCloudImage(_cloudBuilder.Object, _wordReader.Object, _defaultSize);
			var result2 = imageBuilder.BuildImage();
			result1.Should().NotBe(result2);
		}
	}
}