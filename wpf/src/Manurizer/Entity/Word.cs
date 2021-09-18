using System.Collections.Generic;

namespace Manurizer.Entity
{
	public class Word : IIdentable
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Transcription { get; set; }
		public string Class { get; set; }
		public string GuideWord { get; set; }
		public Meaning[] MeaningList { get; set; }
	}
}
