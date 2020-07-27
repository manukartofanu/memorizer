
namespace Manurizer.Core
{
	public interface IQuestion
	{
		string GetQuestion();
		bool CheckCorrectAnswer(string userAnswer);
	}
}
