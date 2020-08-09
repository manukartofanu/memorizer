
namespace Manurizer.Core.Test
{
	public class QuestionMock : IQuestion
	{
		public string Question { get; private set; }
		public string Answer { get; private set; }

		public QuestionMock(string question, string answer)
		{
			Question = question;
			Answer = answer;
		}

		public bool CheckCorrectAnswer(string userAnswer)
		{
			return userAnswer == Answer;
		}

		public string GetQuestionText()
		{
			return Question;
		}
	}
}
