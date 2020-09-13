using Xunit;

namespace Manurizer.Core.Test
{
	public class QuizSequentialTest
	{
		[Fact]
		public void EmptyQuiz()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new Question[0]);
			Assert.Null(quiz.Next());
		}

		[Fact]
		public void CorrectFirstQuestion()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new Question[] { new Question("1", "a"), new Question("2", "b"), new Question("3", "c") });
			Question question = quiz.Next();
			Assert.Equal("1", question.Text);
		}

		[Fact]
		public void CurrectSecondQuestion()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new Question[] { new Question("1", "a"), new Question("2", "b"), new Question("3", "c") });
			quiz.Next();
			Question question = quiz.Next();
			Assert.Equal("2", question.Text);
		}

		[Fact]
		public void CorrectCycle()
		{
			IQuiz quiz = new QuizSequential();
			quiz.Initialize(new Question[] { new Question("1", "a"), new Question("2", "b"), new Question("3", "c") });
			quiz.Next();
			quiz.Next();
			quiz.Next();
			Question question = quiz.Next();
			Assert.Equal("1", question.Text);
		}
	}
}
