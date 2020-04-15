using Manurizer.Commands;
using Manurizer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Manurizer.ViewModels
{
	public class ViewModel : INotifyPropertyChanged
	{
		private WordOld _word = new WordOld();
		private ObservableCollection<WordOld> _words = new ObservableCollection<WordOld>();

		public DelegateCommand AddCommand { get; set; }

		public ViewModel()
		{
			InitializeWords();
			AddCommand = new DelegateCommand(AddWord, (t) => true);
		}

		public void InitializeWords()
		{
			if (File.Exists("save.txt"))
			{
				Words = new ObservableCollection<WordOld>(JsonConvert.DeserializeObject<WordOld[]>(File.ReadAllText("save.txt")));
			}
		}

		public void AddWord(object param)
		{
			Words.Add(Word);
			Word = new WordOld();
		}

		public WordOld Word
		{
			get { return _word; }
			set
			{
				_word = value;
				RaisePropertyChanged(nameof(Word));
			}
		}

		public ObservableCollection<WordOld> Words
		{
			get { return _words; }
			set
			{
				_words = value;
				RaisePropertyChanged(nameof(Words));
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
