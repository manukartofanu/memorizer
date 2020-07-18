using Manurizer.Commands;
using Manurizer.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class TrainViewModel : INotifyPropertyChanged
	{
		private Random _definitionChooser = new Random();
		private Word[] _words = new Word[0];
		private Word _currentWord;
		private Definition _currentDefinition;
		private int _currentIndex;
		private string _answer;
		private bool _showCorrectAnswer;
		public ICommand SubmitAnswerCommand { get; private set; }

		public TrainViewModel()
		{
			SubmitAnswerCommand = new DelegateCommand((t) => { SubmitAnswer(); }, (t) => { return true; });
		}

		public Word[] Words
		{
			get { return _words; }
			set
			{
				_words = value;
				if (_words.Length > 0)
				{
					CurrentWord = Words[_currentIndex];
					CurrentDefinition = CurrentWord.Definitions[_definitionChooser.Next(CurrentWord.Definitions.Count)];
				}
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

		public Definition CurrentDefinition
		{
			get { return _currentDefinition; }
			set
			{
				_currentDefinition = value;
				RaisePropertyChanged(nameof(CurrentDefinition));
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
			if (!ShowCorrectAnswer && CurrentWord.Name == Answer)
			{
				CurrentDefinition.DateLastCorrectAnswer = DateTime.Now.Date;
			}
			if (CurrentWord.Name == Answer || ShowCorrectAnswer)
			{
				Answer = string.Empty;
				_currentIndex++;
				if (_currentIndex >= Words.Length)
				{
					_currentIndex = 0;
				}
				if (Words.Length > 0)
				{
					CurrentWord = Words[_currentIndex];
					CurrentDefinition = CurrentWord.Definitions[_definitionChooser.Next(CurrentWord.Definitions.Count)];
				}
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
