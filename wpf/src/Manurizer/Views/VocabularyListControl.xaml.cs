using Manurizer.Entity;
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
			((VocabularyListViewModel)DataContext).GetWindow += () => { return Window.GetWindow(this); };
		}

		private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (sender is ListBoxItem listBoxItem)
			{
				if (listBoxItem.Content is Word word)
				{
					var command = ((VocabularyListViewModel)DataContext).WordEditCommand;
					if (command.CanExecute(word))
					{
						command.Execute(word);
					}
				}
			}
		}
	}
}
