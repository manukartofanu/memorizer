using Manurizer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Manurizer.Views
{
	/// <summary>
	/// Interaction logic for VocabularyListControl.xaml
	/// </summary>
	public partial class VocabularyListControl : UserControl
	{
		public VocabularyListControl()
		{
			InitializeComponent();
		}

		private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var viewModel = (VocabularyListViewModel)DataContext;
			viewModel.EditWord(Window.GetWindow(this));
		}
	}
}
