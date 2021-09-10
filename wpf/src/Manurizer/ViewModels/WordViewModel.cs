﻿using Manurizer.Commands;
using Manurizer.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class WordViewModel
	{
		public string Name { get; set; }
		public string Class { get; set; }
		public string Transcription { get; set; }
		public string GuideWord { get; set; }
		public MeaningCollection MeaningList { get; set; } = new MeaningCollection();
		public bool IsEdit { get; set; }
		private Word _word;
		public ICommand AddMeaningCommand { get; private set; }
		public ICommand DeleteMeaningCommand { get; private set; }
		public ICommand SaveWordCommand { get; private set; }

		public event Action SaveClicked;

		public WordViewModel()
		{
			AddMeaningCommand = new RelayCommand((t) => { AddMeaning(); });
			DeleteMeaningCommand = new RelayCommand((t) => { DeleteMeaning(t); }, (t) => { return MeaningList.Items.Count > 1; });
			SaveWordCommand = new RelayCommand((t) => { SaveClicked?.Invoke(); });
			MeaningList = new MeaningCollection();
		}

		public WordViewModel(Word word)
			: this()
		{
			_word = word;
			IsEdit = true;
			Name = word.Name;
			Class = word.Category;
			Transcription = word.Transcription;
			GuideWord = word.Meaning;
			MeaningList = new MeaningCollection(word.Definitions);
		}

		private void AddMeaning()
		{
			MeaningList.AddMeaning(new Meaning());
		}

		private void DeleteMeaning(object meaning)
		{
			if (MeaningList.Items.Count > 1)
			{
				MeaningList.DeleteMeaning(meaning as Meaning);
			}
		}

		public Word GetWord()
		{
			return new Word
			{
				Name = Name,
				Category = Class,
				Transcription = Transcription,
				Meaning = GuideWord,
				Definitions = MeaningList.Items.Select(t => new Definition { Text = t.Text, Examples = t.Example }).ToList()
			};
		}

		public void SaveWord()
		{
			_word.Name = Name;
			_word.Category = Class;
			_word.Transcription = Transcription;
			_word.Meaning = GuideWord;
			_word.Definitions = MeaningList.Items.Select(t => new Definition { Text = t.Text, Examples = t.Example }).ToList();
		}

		public class Meaning : INotifyPropertyChanged
		{
			private int _index;
			public string Text { get; set; }
			public string Example { get; set; }

			public Meaning()
			{
			}

			public Meaning(string text, string example)
			{
				Text = text;
				Example = example;
			}

			public int Index
			{
				get { return _index; }
				set
				{
					_index = value;
					RaisePropertyChanged(nameof(Index));
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

		public class MeaningCollection
		{
			public ObservableCollection<Meaning> Items { get; set; }

			public MeaningCollection()
			{
				Items = new ObservableCollection<Meaning>(new Meaning[] { new Meaning { Index = 1 } });
			}

			public MeaningCollection(IEnumerable<Definition> definitions)
			{
				Items = new ObservableCollection<Meaning>(definitions.Select(t => new Meaning(t.Text, t.Examples)));
			}

			public void AddMeaning(Meaning item)
			{
				item.Index = Items.Count + 1;
				Items.Add(item);
			}

			public void DeleteMeaning(Meaning item)
			{
				Items.Remove(item);
				ResetIndexes();
			}

			private void ResetIndexes()
			{
				for (int i = 0; i < Items.Count; ++i)
				{
					Items[i].Index = i + 1;
				}
			}
		}
	}
}
