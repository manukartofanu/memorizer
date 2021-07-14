using Manurizer.Core;
using System;
using System.Collections.Generic;

namespace Manurizer.Events
{
	public static class Communicator
	{
		public static event Action<List<Question>> UpdateWords;
		public static event Action SaveWords;

		public static void RaiseUpdateWords(List<Question> questions)
		{
			UpdateWords?.Invoke(questions);
		}

		public static void RaiseSaveWords()
		{
			SaveWords?.Invoke();
		}
	}
}
