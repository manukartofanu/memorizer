using System;

namespace Manurizer.Entity
{
	public class Meaning : IIdentable
	{
		public long Id { get; set; }
		public long WordId { get; set; }
		public string Text { get; set; }
		public string Example { get; set; }
		public DateTime DateCreated { get; set; }
	}
}
