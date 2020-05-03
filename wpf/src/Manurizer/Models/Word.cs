using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Manurizer.Models
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
		public ObservableCollection<Definition> Definitions { get; set; } = new ObservableCollection<Definition>();
	}
}
