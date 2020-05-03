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
		}
	}
}
