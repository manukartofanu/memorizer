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
		private Question _currentQuestion;
		private string _answer;
		private bool _showCorrectAnswer;
		public ICommand SubmitAnswerCommand { get; private set; }

		public TrainViewModel()
		{
			SubmitAnswerCommand = new DelegateCommand((t) => { SubmitAnswer(); }, (t) => { return true; });
		}

		public void InitializeQuiz()
		{
			_quiz.Initialize(Transformer.ToQuestionList(Words));
			CurrentQuestion = (Question)_quiz.Next();
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

		public Question CurrentQuestion
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
			if (CurrentQuestion.CorrectAnswer == Answer || ShowCorrectAnswer)
			{
				Answer = string.Empty;
				CurrentQuestion = (Question)_quiz.Next();
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
