using Manurizer.Events;
using Manurizer.ViewModels;
using System.Windows;

namespace Manurizer.Views
{
	/// <summary>
	/// Interaction logic for TrainWindow.xaml
	/// </summary>
	public partial class TrainWindow : Window
	{
		public TrainWindow(object window, TrainViewModel viewModel)
		{
			InitializeComponent();
			Owner = (Window)window;
			DataContext = viewModel;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var viewModel = (TrainViewModel)DataContext;
			Communicator.RaiseUpdateWords(viewModel.GetQuestions());
		}
	}
}
