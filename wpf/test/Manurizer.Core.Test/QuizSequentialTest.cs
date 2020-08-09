using Xunit;

namespace Manurizer.Core.Test
{
	public class QuizSequentialTest
	{
		[Fact]
		public void EmptyQuiz()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new IQuestion[0]);
			Assert.Null(quiz.Next());
		}

		[Fact]
		public void CorrectFirstQuestion()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new IQuestion[] { new QuestionMock("1", "a"), new QuestionMock("2", "b"), new QuestionMock("3", "c") });
			IQuestion question = quiz.Next();
			Assert.Equal("1", question.GetQuestionText());
		}

		[Fact]
		public void CurrectSecondQuestion()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new IQuestion[] { new QuestionMock("1", "a"), new QuestionMock("2", "b"), new QuestionMock("3", "c") });
			quiz.Next();
			IQuestion question = quiz.Next();
			Assert.Equal("2", question.GetQuestionText());
		}

		[Fact]
		public void CorrectCycle()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new IQuestion[] { new QuestionMock("1", "a"), new QuestionMock("2", "b"), new QuestionMock("3", "c") });
			quiz.Next();
			quiz.Next();
			quiz.Next();
			IQuestion question = quiz.Next();
			Assert.Equal("1", question.GetQuestionText());
		}
	}
}
