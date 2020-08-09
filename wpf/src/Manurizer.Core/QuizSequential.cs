using System.Collections.Generic;
using System.Linq;

namespace Manurizer.Core
{
	public class QuizSequential : IQuiz
	{
		private IQuestion[] _questions;
		private int _currentQuestionNumber;

		public void Initialize(IEnumerable<IQuestion> questions)
		{
			_questions = questions.ToArray();
			_currentQuestionNumber = -1;
		}

		public IQuestion Next()
		{
			_currentQuestionNumber++;
			if (_currentQuestionNumber < _questions.Length)
			{
				return _questions[_currentQuestionNumber];
			}
			else
			{
				_currentQuestionNumber = 0;
				if (_currentQuestionNumber < _questions.Length)
				{
					return _questions[_currentQuestionNumber];
				}
				else
				{
					return null;
				}
			}
		}

		public List<Question> GetQuestions()
		{
			return _questions.ToList().Cast<Question>().ToList();
		}
	}
}
