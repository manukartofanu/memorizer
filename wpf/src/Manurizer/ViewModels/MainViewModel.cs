using Manurizer.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Manurizer.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private bool _isNavigatorExpanded = true;
		private bool _isFirstVisible = true;
		private bool _isSecondVisible;
		private bool _isThirdVisible;
		private bool _isFourthVisible;
		
		public ICommand NavigatorExpandCommand { get; private set; }
		public ICommand SelectControlCommand { get; private set; }

		public MainViewModel()
		{
			NavigatorExpandCommand = new RelayCommand((t) => { IsNavigatorExpanded = !IsNavigatorExpanded;});
			SelectControlCommand = new RelayCommand((t) => { SelectControl((int)t); });
		}

		private void SelectControl(int index)
		{
			IsFirstVisible = false;
			IsSecondVisible = false;
			IsThirdVisible = false;
			IsFourthVisible = false;
			switch (index)
			{
				case 0:
					IsFirstVisible = true;
					break;
				case 1:
					IsSecondVisible = true;
					break;
				case 2:
					IsThirdVisible = true;
					break;
				case 3:
					IsFourthVisible = true;
					break;
			}
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

		public bool IsFirstVisible
		{
			get { return _isFirstVisible; }
			set
			{
				_isFirstVisible = value;
				RaisePropertyChanged(nameof(IsFirstVisible));
			}
		}

		public bool IsSecondVisible
		{
			get { return _isSecondVisible; }
			set
			{
				_isSecondVisible = value;
				RaisePropertyChanged(nameof(IsSecondVisible));
			}
		}

		public bool IsThirdVisible
		{
			get { return _isThirdVisible; }
			set
			{
				_isThirdVisible = value;
				RaisePropertyChanged(nameof(IsThirdVisible));
			}
		}

		public bool IsFourthVisible
		{
			get { return _isFourthVisible; }
			set
			{
				_isFourthVisible = value;
				RaisePropertyChanged(nameof(IsFourthVisible));
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
