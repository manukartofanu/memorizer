using Newtonsoft.Json;
using System;

namespace Manurizer.Core
{
	public class Definition
	{
		[JsonProperty(PropertyName = "t")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "e")]
		public string Examples { get; set; }

		[JsonProperty(PropertyName = "dc")]
		public DateTime? DateCreated { get; set; }

		[JsonProperty(PropertyName = "d")]
		public DateTime? LastCorrectAnswerDate { get; set; }
	}
}
