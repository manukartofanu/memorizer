using Manurizer.Commands;
using Manurizer.Core;
using Manurizer.Events;
using Manurizer.Model;
using Manurizer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class VocabularyListViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<Word> _words = new ObservableCollection<Word>();
		private Word _selectedWord;
		public ICommand WordAddCommand { get; private set; }
		public ICommand WordCopyCommand { get; private set; }
		public ICommand WordDeleteCommand { get; private set; }
		public ICommand TrainCommand { get; private set; }

		public VocabularyListViewModel()
		{
			Words = new ObservableCollection<Word>(WordLoaderSaver.Words);
			WordAddCommand = new DelegateCommand((t) => { AddWord(t); }, (t) => { return true; });
			WordCopyCommand = new DelegateCommand((t) => { CopyWord(); }, (t) => { return true; });
			WordDeleteCommand = new DelegateCommand((t) => { DeleteWord(); }, (t) => { return true; });
			TrainCommand = new DelegateCommand((t) => { Train(t); }, (t) => { return true; });
			Communicator.UpdateWords += UpdateWords;
			Communicator.SaveWords += () => { WordLoaderSaver.Save(Words.ToArray()); } ;
		}

		private void AddWord(object window)
		{
			var viewModel = new WordViewModel { Word = new Word() };
			new WordWindow(window, viewModel).ShowDialog();
			Words.Add(viewModel.Word);
		}

		internal void EditWord(object window)
		{
			if (SelectedWord != null)
			{
				var viewModel = new WordViewModel { Word = SelectedWord };
				new WordWindow(window, viewModel).ShowDialog();
			}
		}

		private void CopyWord()
		{
			if (SelectedWord != null)
			{
				Words.Add(new Word(SelectedWord));
			}
		}

		private void DeleteWord()
		{
			if (SelectedWord != null)
			{
				Words.Remove(SelectedWord);
			}
		}

		private void Train(object window)
		{
			var viewModel = new TrainViewModel { Words = Words.Where(t => t.Definitions.Count > 0).ToArray() };
			viewModel.InitializeQuiz();
			new TrainWindow(window, viewModel).ShowDialog();
		}

		private void UpdateWords(List<Question> questions)
		{
			Transformer.UpdateWords(Words, questions);
		}

		public ObservableCollection<Word> Words
		{
			get { return _words; }
			set
			{
				_words = value;
				RaisePropertyChanged(nameof(Words));
			}
		}

		public Word SelectedWord
		{
			get { return _selectedWord; }
			set
			{
				_selectedWord = value;
				RaisePropertyChanged(nameof(SelectedWord));
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
