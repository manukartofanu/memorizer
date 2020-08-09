
namespace Manurizer.Core
{
	public interface IQuestion
	{
		string GetQuestionText();
		bool CheckCorrectAnswer(string userAnswer);
	}
}
