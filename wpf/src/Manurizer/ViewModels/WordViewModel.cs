using Manurizer.Commands;
using Manurizer.Core;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class WordViewModel : INotifyPropertyChanged
	{
		private Word _word = new Word();
		public ICommand DefinitionAddCommand { get; private set; }

		public WordViewModel()
		{
			DefinitionAddCommand = new DelegateCommand((t) => { AddDefinition(); }, (t) => { return true; });
		}

		private void AddDefinition()
		{
			Word.Definitions.Add(new Definition());
		}

		public Word Name { get; set; }

		public Word Word
		{
			get { return _word; }
			set
			{
				_word = value;
				RaisePropertyChanged(nameof(Word));
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
