using Manurizer.ViewModels;
using System.Windows;

namespace Manurizer.Views
{
	/// <summary>
	/// Interaction logic for WordWindow.xaml
	/// </summary>
	public partial class WordWindow : Window
	{
		public WordWindow(object window, WordViewModel viewModel)
		{
			InitializeComponent();
			Owner = (Window)window;
			DataContext = viewModel;
			viewModel.MeaningAdded += SelectLastMeaningTab;
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void SelectLastMeaningTab()
		{
			tabControlMeaning.SelectedIndex = tabControlMeaning.Items.Count - 1;
		}
	}
}
