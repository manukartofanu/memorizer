using Manurizer.Commands;
using Manurizer.Core;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class TrainViewModel : INotifyPropertyChanged
	{
		private QuizSequential _quiz = new QuizSequential();
		private Word[] _words = new Word[0];
		private Word _currentWord;
		private string _currentQuestion;
		private string _answer;
		private bool _showCorrectAnswer;
		public ICommand SubmitAnswerCommand { get; private set; }

		public TrainViewModel()
		{
			SubmitAnswerCommand = new DelegateCommand((t) => { SubmitAnswer(); }, (t) => { return true; });
		}

		public void InitializeQuiz()
		{
			_quiz.Initialize(Words);
			CurrentWord = (Word)_quiz.Next();
			CurrentQuestion = CurrentWord?.GetQuestion();
		}

		public Word[] Words
		{
			get { return _words; }
			set
			{
				_words = value;
				RaisePropertyChanged(nameof(Words));
			}
		}

		public Word CurrentWord
		{
			get { return _currentWord; }
			set
			{
				_currentWord = value;
				RaisePropertyChanged(nameof(CurrentWord));
			}
		}

		public string CurrentQuestion
		{
			get { return _currentQuestion; }
			set
			{
				_currentQuestion = value;
				RaisePropertyChanged(nameof(CurrentQuestion));
			}
		}

		public string Answer
		{
			get { return _answer; }
			set
			{
				_answer = value;
				RaisePropertyChanged(nameof(Answer));
			}
		}

		public bool ShowCorrectAnswer
		{
			get { return _showCorrectAnswer; }
			set
			{
				_showCorrectAnswer = value;
				RaisePropertyChanged(nameof(ShowCorrectAnswer));
			}
		}

		private void SubmitAnswer()
		{
			if (CurrentWord.Name == Answer || ShowCorrectAnswer)
			{
				Answer = string.Empty;
				CurrentWord = (Word)_quiz.Next();
				CurrentQuestion = CurrentWord?.GetQuestion();
				ShowCorrectAnswer = false;
			}
			else
			{
				ShowCorrectAnswer = true;
			}
		}

		#region INotifyPropertyChanged

		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
