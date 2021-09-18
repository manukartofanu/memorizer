using System;
using System.Collections.Generic;
using System.Linq;

namespace Manurizer.Core
{
	public class QuizTimed : IQuiz
	{
		private Question[] _questions;
		private int _currentQuestionIndex;
		private int[] _intervals = new[]
		{
			1, 2, 3, 4, 5, 6, 7, 9, 11, 13, 17, 21, 29,
			36, 43, 50, 57, 64, 71, 91, 111, 131, 151,
			171, 191, 211, 241, 271, 301, 331, 361, 391,
			421, 451, 501, 561, 621, 711, 801, 891, 981
		};
		private List<RepeatInfo> _repeatCount = new List<RepeatInfo>();

		public void Initialize(IEnumerable<Question> questions)
		{
			_questions = questions.ToArray();
			for (int i = 0; i < _questions.Length; ++i)
			{
				int expectedCount = (DateTime.Now - (_questions[i].DateCreated)).Days;
				if (_questions[i].DateCreated == null)
				{
					_questions[i].DateCreated = DateTime.Now;
				}
				int diff = expectedCount - _questions[i].CorrectAnswerCount;
				if (diff > 0)
				{
					_repeatCount.Add(new RepeatInfo(i, diff));
				}
			}
			_currentQuestionIndex = -1;
		}

		public Question Next()
		{
			_currentQuestionIndex++;
			if (_repeatCount.Count == 0)
			{
				return null;
			}
			if (_currentQuestionIndex < _repeatCount.Count)
			{
				return _questions[_repeatCount[_currentQuestionIndex].Index];
			}
			else
			{
				_currentQuestionIndex = 0;
				if (_currentQuestionIndex < _repeatCount.Count)
				{
					return _questions[_repeatCount[_currentQuestionIndex].Index];
				}
				else
				{
					return null;
				}
			}
		}

		public bool CheckCorrectAnswer(string userAnswer)
		{
			if (_repeatCount.Count == 0)
			{
				return false;
			}
			var isCorrect = _questions[_repeatCount[_currentQuestionIndex].Index].CheckCorrectAnswer(userAnswer);
			if (isCorrect)
			{
				_repeatCount[_currentQuestionIndex].Count--;
				if (_repeatCount[_currentQuestionIndex].Count <= 0)
				{
					_repeatCount.RemoveAt(_currentQuestionIndex);
				}
			}
			return isCorrect;
		}

		public List<Question> GetQuestions()
		{
			return _questions.ToList().Cast<Question>().ToList();
		}

		private class RepeatInfo
		{
			public int Index { get; set; }
			public int Count { get; set; }

			public RepeatInfo(int index, int count)
			{
				Index = index;
				Count = count;
			}
		}
	}
}
