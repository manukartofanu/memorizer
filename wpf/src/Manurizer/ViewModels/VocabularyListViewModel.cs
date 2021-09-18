using Manurizer.Commands;
using Manurizer.Entity;
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
			WordAddCommand = new RelayCommand((t) => { AddWord(); });
			WordCopyCommand = new RelayCommand((t) => { CopyWord(t); });
			WordEditCommand = new RelayCommand((t) => { EditWord(t); });
			WordDeleteCommand = new RelayCommand((t) => { DeleteWord(t); });
			TrainCommand = new RelayCommand((t) => { Train(t); });
			Communicator.UpdateWords += UpdateWords;
		}

		private void AddWord()
		{
			var viewModel = new WordViewModel();
			var dialog = new WordWindow(GetWindow(), viewModel);
			viewModel.SaveClicked += () =>
			{
				Word word = viewModel.GetWord();
				foreach (var item in word.MeaningList)
				{
					item.DateCreated = DateTime.Now;
				}
				Words.Add(word);
				WordLoaderSaver.SaveWord(word);
				dialog.Close();
			};
			dialog.ShowDialog();
		}

		internal void EditWord(object item)
		{
			if (item is Word word)
			{
				var viewModel = new WordViewModel(word);
				var dialog = new WordWindow(GetWindow(), viewModel);
				viewModel.SaveClicked += () =>
				{
					viewModel.SaveWord();
					dialog.Close();
				};
				dialog.ShowDialog();
			}
		}

		private void CopyWord(object item)
		{
			if (item is Word word)
			{
				var viewModel = new WordViewModel { Name = word.Name, Class = word.Class, Transcription = word.Transcription };
				var dialog = new WordWindow(GetWindow(), viewModel);
				viewModel.SaveClicked += () =>
				{
					Word newWord = viewModel.GetWord();
					foreach (var meaning in newWord.MeaningList)
					{
						meaning.DateCreated = DateTime.Now;
					}
					Words.Add(newWord);
					WordLoaderSaver.SaveWord(newWord);
					dialog.Close();
				};
				dialog.ShowDialog();
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
			var viewModel = new TrainViewModel { Words = Words.Where(t => t.MeaningList.Length > 0).ToArray() };
			viewModel.InitializeQuiz();
			new TrainWindow(window, viewModel).ShowDialog();
		}

		private void UpdateWords(List<Core.Question> questions)
		{
			Core.Transformer.UpdateWords(Words, questions);
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
