using Newtonsoft.Json;
using System.Collections.Generic;

namespace Manurizer.Models
{
	public class Word
	{
		public Word()
		{
		}

		public Word(WordOld old)
		{
			Name = old.Text;
			Category = old.Class;
			Definitions = new List<Definition> { new Definition { Text = old.Definition } };
		}

		[JsonProperty(PropertyName = "n")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "t")]
		public string Transcription { get; set; }

		[JsonProperty(PropertyName = "c")]
		public string Category { get; set; }

		[JsonProperty(PropertyName = "m")]
		public string Meaning { get; set; }

		[JsonProperty(PropertyName = "d")]
		public List<Definition> Definitions { get; set; }
	}
}
