using Manurizer.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private bool _isNavigatorExpanded = true;
		
		public ICommand NavigatorExpandCommand { get; private set; }

		public MainViewModel()
		{
			NavigatorExpandCommand = new RelayCommand((t) => { IsNavigatorExpanded = !IsNavigatorExpanded;});
		}

		public bool IsNavigatorExpanded
		{
			get { return _isNavigatorExpanded; }
			set
			{
				_isNavigatorExpanded = value;
				RaisePropertyChanged(nameof(IsNavigatorExpanded));
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
