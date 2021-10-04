using Manurizer.Commands;
using Manurizer.Entity;
using Manurizer.Entity.Database;
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
		private ObservableCollection<WordModel> _words = new ObservableCollection<WordModel>();
		private WordModel _selectedWord;
		public ICommand WordAddCommand { get; private set; }
		public ICommand WordCopyCommand { get; private set; }
		public ICommand WordEditCommand { get; private set; }
		public ICommand WordDeleteCommand { get; private set; }
		public ICommand TrainCommand { get; private set; }

		public Func<Window> GetWindow;

		public VocabularyListViewModel()
		{
			Words = new ObservableCollection<WordModel>(WordLoaderSaver.Words.Select(t => new WordModel(t)));
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
				WordLoaderSaver.SaveWord(word);
				Words = new ObservableCollection<WordModel>(WordLoaderSaver.Words.Select(t => new WordModel(t)));
				dialog.Close();
			};
			dialog.ShowDialog();
		}

		internal void EditWord(object item)
		{
			if (item is WordModel word)
			{
				var viewModel = new WordViewModel(word.Word);
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
			if (item is WordModel word)
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
					WordLoaderSaver.SaveWord(newWord);
					Words = new ObservableCollection<WordModel>(WordLoaderSaver.Words.Select(t => new WordModel(t)));
					dialog.Close();
				};
				dialog.ShowDialog();
			}
		}

		private void DeleteWord(object item)
		{
			if (item is WordModel word)
			{
				using (IWordRepository repository = new WordRepository(DatabaseSourceDefinitor.ConnectionString))
				{
					repository.DeleteItem(word.Word.Id);
					Words = new ObservableCollection<WordModel>(repository.GetAllItemsEx().OrderBy(t => t.GuideWord).OrderBy(t => t.Name).Select(t => new WordModel(t)));
				}
			}
		}

		private void Train(object window)
		{
			var viewModel = new TrainViewModel { Words = Words.Where(t => t.Word.MeaningList.Length > 0).Select(t => t.Word).ToArray() };
			viewModel.InitializeQuiz();
			new TrainWindow(window, viewModel).ShowDialog();
		}

		private void UpdateWords(List<Core.Question> questions)
		{
			Core.Transformer.UpdateWords(Words.Select(t => t.Word).ToArray(), questions);
		}

		public ObservableCollection<WordModel> Words
		{
			get { return _words; }
			set
			{
				_words = value;
				RaisePropertyChanged(nameof(Words));
			}
		}

		public WordModel SelectedWord
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
