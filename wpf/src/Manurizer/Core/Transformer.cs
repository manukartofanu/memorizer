using System.Collections.Generic;

namespace Manurizer.Core
{
	public class Transformer
	{
		public static List<Question> ToQuestionList(Word[] words)
		{
			var result = new List<Question>();
			for (int wordIndex = 0; wordIndex < words.Length; ++wordIndex)
			{
				var word = words[wordIndex];
				for (int definitionIndex = 0; definitionIndex < words[wordIndex].Definitions.Count; ++definitionIndex)
				{
					var definition = words[wordIndex].Definitions[definitionIndex];
					result.Add(new Question(word.Definitions[definitionIndex].Text, word.Name, definition.DateCreated, definition.CorrectAnswerCount, wordIndex, definitionIndex));
				}
			}
			return result;
		}

		public static void UpdateWords(IList<Word> words, List<Question> questions)
		{
			for (int i = 0; i < questions.Count; ++i)
			{
				var question = questions[i];
				words[question.WordIndex].Definitions[question.DefinitionIndex].CorrectAnswerCount = question.CorrectAnswerCount;
				words[question.WordIndex].Definitions[question.DefinitionIndex].DateCreated = question.DateCreated;
			}
		}
	}
}
