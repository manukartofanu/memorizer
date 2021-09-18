using Manurizer.Commands;
using Manurizer.Entity;
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
			Class = word.Class;
			Transcription = word.Transcription;
			GuideWord = word.GuideWord;
			MeaningList = new MeaningCollection(word.MeaningList);
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
				Class = Class,
				Transcription = Transcription,
				GuideWord = GuideWord,
				MeaningList = MeaningList.Items.Select(t => new Entity.Meaning { Text = t.Text, Example = t.Example }).ToArray()
			};
		}

		public void SaveWord()
		{
			_word.Name = Name;
			_word.Class = Class;
			_word.Transcription = Transcription;
			_word.GuideWord = GuideWord;
			_word.MeaningList = MeaningList.Items.Select(t => new Entity.Meaning { Text = t.Text, Example = t.Example }).ToArray();
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

			public MeaningCollection(IEnumerable<Entity.Meaning> meanings)
			{
				Items = new ObservableCollection<Meaning>(meanings.Select(t => new Meaning(t.Text, t.Example)));
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
