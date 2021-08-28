using Newtonsoft.Json;
using System.Collections.Generic;

namespace Manurizer.Core
{
	public class Word
	{
		[JsonProperty(PropertyName = "n")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "t")]
		public string Transcription { get; set; }

		[JsonProperty(PropertyName = "c")]
		public string Category { get; set; }

		[JsonProperty(PropertyName = "m")]
		public string Meaning { get; set; }

		[JsonProperty(PropertyName = "d")]
		public List<Definition> Definitions { get; set; } = new List<Definition>();

		public Word()
		{
		}

		public Word(Word word)
		{
			Name = word.Name;
			Transcription = word.Transcription;
			Category = word.Category;
			Meaning = word.Meaning;
			foreach (var item in word.Definitions)
			{
				Definitions.Add(new Definition { Text = item.Text, Examples = item.Examples });
			}
		}
	}
}
