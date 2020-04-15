using Newtonsoft.Json;

namespace Manurizer.Models
{
	public class Definition
	{
		[JsonProperty(PropertyName = "t")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "e")]
		public string Examples { get; set; }
	}
}
