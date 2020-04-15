using Newtonsoft.Json;

namespace Manurizer.Models
{
	public class WordOld
	{
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "class")]
		public string Class { get; set; }

		[JsonProperty(PropertyName = "def")]
		public string Definition { get; set; }
	}
}
