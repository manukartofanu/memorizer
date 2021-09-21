using Manurizer.Entity;

namespace Manurizer.Model
{
	public class WordModel
	{
		public Word Word { get; set; }
		public string Name { get { return Word.Name; } }
		public string Transcription { get { return Word.Transcription; } }
		public string Class { get { return Word.Class; } }
		public string GuideWord { get { return Word.GuideWord; } }
		public string FirstMeaning { get { return Word.MeaningList[0]?.Text ?? string.Empty; } }

		public WordModel(Word word)
		{
			Word = word;
		}
	}
}
