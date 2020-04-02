﻿using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Manurizer
{
	public class ViewModel : INotifyPropertyChanged
	{
		private Word _word = new Word();
		private ObservableCollection<Word> _words = new ObservableCollection<Word>();

		public DelegateCommand AddCommand { get; set; }

		public ViewModel()
		{
			Words = new ObservableCollection<Word>(JsonConvert.DeserializeObject<Word[]>(File.ReadAllText(@"save.txt")));
			AddCommand = new DelegateCommand(AddWord, (t) => true);
		}

		public void AddWord(object param)
		{
			Words.Add(Word);
			Word = new Word();
		}

		public Word Word
		{
			get { return _word; }
			set
			{
				_word = value;
				RaisePropertyChanged(nameof(Word));
			}
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
