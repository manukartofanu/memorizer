using System;

namespace Manurizer.Core
{
	public class Question
	{
		public string Text { get; private set; }
		public string CorrectAnswer { get; private set; }
		public string UserAnswer { get; private set; }
		public DateTime? CorrectAnswerDate { get; private set; }
		public int WordIndex { get; private set; }
		public int DefinitionIndex { get; private set; }

		public Question(string text, string answer)
		{
			Text = text;
			CorrectAnswer = answer;
		}

		public Question(string text, string answer, int wordIndex, int definitionIndex)
		{
			Text = text;
			CorrectAnswer = answer;
			WordIndex = wordIndex;
			DefinitionIndex = definitionIndex;
		}

		public bool CheckCorrectAnswer(string userAnswer)
		{
			UserAnswer = userAnswer;
			bool result = userAnswer == CorrectAnswer;
			if (result)
			{
				CorrectAnswerDate = DateTime.Now;
			}
			return result;
		}
	}
}
