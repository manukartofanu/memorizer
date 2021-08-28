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
using System.Windows;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class VocabularyListViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<Word> _words = new ObservableCollection<Word>();
		private Word _selectedWord;
		public ICommand WordAddCommand { get; private set; }
		public ICommand WordCopyCommand { get; private set; }
		public ICommand WordEditCommand { get; private set; }
		public ICommand WordDeleteCommand { get; private set; }
		public ICommand TrainCommand { get; private set; }

		public Func<Window> GetWindow;

		public VocabularyListViewModel()
		{
			Words = new ObservableCollection<Word>(WordLoaderSaver.Words);
			WordAddCommand = new DelegateCommand((t) => { AddWord(); }, (t) => { return true; });
			WordCopyCommand = new DelegateCommand((t) => { CopyWord(t); }, (t) => { return true; });
			WordEditCommand = new DelegateCommand((t) => { EditWord(t); }, (t) => { return true; });
			WordDeleteCommand = new DelegateCommand((t) => { DeleteWord(t); }, (t) => { return true; });
			TrainCommand = new DelegateCommand((t) => { Train(t); }, (t) => { return true; });
			Communicator.UpdateWords += UpdateWords;
			Communicator.SaveWords += () => { WordLoaderSaver.Save(Words.ToArray()); } ;
		}

		private void AddWord()
		{
			var viewModel = new WordViewModel();
			new WordWindow(GetWindow(), viewModel).ShowDialog();
			Words.Add(viewModel.GetWord());
		}

		internal void EditWord(object item)
		{
			if (item is Word word)
			{
				var viewModel = new WordViewModel(word);
				new WordWindow(GetWindow(), viewModel).ShowDialog();
				viewModel.SaveWord();
			}
		}

		private void CopyWord(object item)
		{
			if (item is Word word)
			{
				Words.Add(new Word(word));
			}
		}

		private void DeleteWord(object item)
		{
			if (item is Word word)
			{
				Words.Remove(word);
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
