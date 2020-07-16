using Newtonsoft.Json;
using System;

namespace Manurizer.Models
{
	public class Definition
	{
		[JsonProperty(PropertyName = "t")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "e")]
		public string Examples { get; set; }

		[JsonProperty(PropertyName = "c")]
		public DateTime? DateLastCorrectAnswer { get; set; }
	}
}
