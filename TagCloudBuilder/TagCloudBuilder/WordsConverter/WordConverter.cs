namespace TagCloudBuilder.WordsConverter
{
	public class WordConverter : IWordConverter
	{
		public string ConvertWord(string word)
		{
			return word.ToLower();
		}
	}
}