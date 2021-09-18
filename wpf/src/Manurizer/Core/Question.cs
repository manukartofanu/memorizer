using System;

namespace Manurizer.Core
{
	public class Question
	{
		public string Text { get; private set; }
		public string CorrectAnswer { get; private set; }
		public string UserAnswer { get; private set; }
		public DateTime DateCreated { get; set; }
		public int CorrectAnswerCount { get; set; }
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

		public Question(string text, string answer, DateTime dateCreated, int wordIndex, int definitionIndex)
		{
			Text = text;
			CorrectAnswer = answer;
			DateCreated = dateCreated;
			WordIndex = wordIndex;
			DefinitionIndex = definitionIndex;
		}

		public bool CheckCorrectAnswer(string userAnswer)
		{
			UserAnswer = userAnswer;
			bool result = userAnswer == CorrectAnswer;
			if (result)
			{
				CorrectAnswerCount++;
			}
			return result;
		}
	}
}
