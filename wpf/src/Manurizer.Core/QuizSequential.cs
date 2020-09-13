using System.Collections.Generic;
using System.Linq;

namespace Manurizer.Core
{
	public class QuizSequential : IQuiz
	{
		private Question[] _questions;
		private int _currentQuestionNumber;

		public void Initialize(IEnumerable<Question> questions)
		{
			_questions = questions.ToArray();
			_currentQuestionNumber = -1;
		}

		public Question Next()
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
