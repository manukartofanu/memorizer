
using Newtonsoft.Json;

namespace Manurizer
{
	public class Word
	{
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "def")]
		public string Definition { get; set; }

		[JsonProperty(PropertyName = "class")]
		public string Class { get; set; }
	}
}
