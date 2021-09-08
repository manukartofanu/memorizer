using Manurizer.Commands;
using Manurizer.Core;
using System;
using System.Collections.ObjectModel;
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
		public ObservableCollection<Meaning> MeaningList { get; set; } = new ObservableCollection<Meaning>();
		public bool IsEdit { get; set; }
		private Word _word;
		public ICommand AddMeaningCommand { get; private set; }
		public ICommand DeleteMeaningCommand { get; private set; }
		public ICommand SaveWordCommand { get; private set; }

		public event Action SaveClicked;

		public WordViewModel()
		{
			AddMeaningCommand = new DelegateCommand((t) => { AddMeaning(); }, (t) => { return true; });
			DeleteMeaningCommand = new DelegateCommand((t) => { DeleteMeaning(t); }, (t) => { return true; });
			SaveWordCommand = new DelegateCommand((t) => { SaveClicked?.Invoke(); }, (t) => { return true; });
			MeaningList = new ObservableCollection<Meaning>(new Meaning[] { new Meaning() });
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
			MeaningList = new ObservableCollection<Meaning>(word.Definitions.Select(t => new Meaning(t.Text, t.Examples)).ToList());
		}

		private void AddMeaning()
		{
			if (MeaningList == null)
			{
				MeaningList = new ObservableCollection<Meaning>();
			}
			MeaningList.Add(new Meaning());
		}

		private void DeleteMeaning(object meaning)
		{
			if (MeaningList != null && MeaningList.Count > 1)
			{
				MeaningList.Remove(meaning as Meaning);
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
				Definitions = MeaningList.Select(t => new Definition { Text = t.Text, Examples = t.Example }).ToList()
			};
		}

		public void SaveWord()
		{
			_word.Name = Name;
			_word.Category = Class;
			_word.Transcription = Transcription;
			_word.Meaning = GuideWord;
			_word.Definitions = MeaningList.Select(t => new Definition { Text = t.Text, Examples = t.Example }).ToList();
		}

		public class Meaning
		{
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
		}
	}
}
