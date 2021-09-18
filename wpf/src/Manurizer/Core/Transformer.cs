using System.Collections.Generic;

namespace Manurizer.Core
{
	public class Transformer
	{
		public static List<Question> ToQuestionList(Entity.Word[] words)
		{
			var result = new List<Question>();
			for (int wordIndex = 0; wordIndex < words.Length; ++wordIndex)
			{
				var word = words[wordIndex];
				for (int definitionIndex = 0; definitionIndex < words[wordIndex].MeaningList.Length; ++definitionIndex)
				{
					var definition = words[wordIndex].MeaningList[definitionIndex];
					result.Add(new Question(word.MeaningList[definitionIndex].Text, word.Name, definition.DateCreated, wordIndex, definitionIndex));
				}
			}
			return result;
		}

		public static void UpdateWords(IList<Entity.Word> words, List<Question> questions)
		{
			for (int i = 0; i < questions.Count; ++i)
			{
				var question = questions[i];
				words[question.WordIndex].MeaningList[question.DefinitionIndex].DateCreated = question.DateCreated;
			}
		}
	}
}
