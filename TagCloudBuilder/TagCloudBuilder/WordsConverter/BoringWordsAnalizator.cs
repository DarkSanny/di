using System.Linq;

namespace TagCloudBuilder.WordsConverter
{
	public class BoringWordsAnalizator : IWordAnalizator
	{

		//можно сделать конструктор, принимающий IWordReader и получать список слов из него
		private readonly string[] _boringWords =
		{
			"в",
			"без",
			"до",
			"из",
			"к",
			"на",
			"по",
			"о",
			"от",
			"перед",
			"при",
			"через",
			"с",
			"у",
			"за",
			"над",
			"об",
			"под",
			"для",
			"и"
		};

		public bool IsCorrectWord(string word)
		{
			return !_boringWords.Contains(word);
		}
	}
}
