using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Manurizer.Core.Test
{
	public class TransformerToQuestionTest
	{
		[Fact]
		public void EmptyWordArray()
		{
			Word[] wordArray = new Word[0];
			List<Question> questionList = Transformer.ToQuestionList(wordArray);
			Assert.Empty(questionList);
		}

		[Fact]
		public void WordWith1Definition()
		{
			Word[] wordArray = new Word[] {
				new Word
				{
					Name = "a",
					Definitions = new ObservableCollection<Definition> { new Definition { Text = "da" } }
				}
			};
			List<Question> questionList = Transformer.ToQuestionList(wordArray);
			Assert.NotEmpty(questionList);
			Assert.True(QuestionEqual(new Question("da", "a", 0, 0), questionList[0]));
		}

		[Fact]
		public void WordsWith1Definition()
		{
			Word[] wordArray = new Word[] {
				new Word
				{
					Name = "a",
					Definitions = new ObservableCollection<Definition> { new Definition { Text = "da" } }
				},
				new Word
				{
					Name = "b",
					Definitions = new ObservableCollection<Definition> { new Definition { Text = "db"} }
				}
			};
			List<Question> questionList = Transformer.ToQuestionList(wordArray);
			Assert.Equal(2, questionList.Count);
			Assert.True(QuestionEqual(new Question("da", "a", 0, 0), questionList[0]));
			Assert.True(QuestionEqual(new Question("db", "b", 1, 0), questionList[1]));
		}

		[Fact]
		public void WordWith2Definitions()
		{
			Word[] wordArray = new Word[] {
				new Word
				{
					Name = "a",
					Definitions = new ObservableCollection<Definition> { new Definition { Text = "da1" }, new Definition { Text = "da2" } }
				}
			};
			List<Question> questionList = Transformer.ToQuestionList(wordArray);
			Assert.Equal(2, questionList.Count);
			Assert.True(QuestionEqual(new Question("da1", "a", 0, 0), questionList[0]));
			Assert.True(QuestionEqual(new Question("da2", "a", 0, 1), questionList[1]));
		}

		[Fact]
		public void WordsWith2Definitions()
		{
			Word[] wordArray = new Word[] {
				new Word
				{
					Name = "a",
					Definitions = new ObservableCollection<Definition> { new Definition { Text = "da1" }, new Definition { Text = "da2" } }
				},
				new Word
				{
					Name = "b",
					Definitions = new ObservableCollection<Definition> { new Definition { Text = "db1" }, new Definition { Text = "db2" } }
				}
			};
			List<Question> questionList = Transformer.ToQuestionList(wordArray);
			Assert.Equal(4, questionList.Count);
			Assert.True(QuestionEqual(new Question("da1", "a", 0, 0), questionList[0]));
			Assert.True(QuestionEqual(new Question("da2", "a", 0, 1), questionList[1]));
			Assert.True(QuestionEqual(new Question("db1", "b", 1, 0), questionList[2]));
			Assert.True(QuestionEqual(new Question("db2", "b", 1, 1), questionList[3]));
		}

		private bool QuestionEqual(Question q1, Question q2)
		{
			return q1.Text == q2.Text && q1.CorrectAnswer == q2.CorrectAnswer
				&& q1.WordIndex == q2.WordIndex && q1.DefinitionIndex == q2.DefinitionIndex;
		}
	}
}
