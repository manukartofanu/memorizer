using Manurizer.Core;
using Manurizer.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Manurizer.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var viewModel = (MainViewModel)DataContext;
			var saveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Manurizer");
			if (!Directory.Exists(saveDirectory))
			{
				Directory.CreateDirectory(saveDirectory);
			}
			File.WriteAllText(Path.Combine(saveDirectory, "save.txt"), JsonConvert.SerializeObject(viewModel.Words));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Manurizer", "save.txt");
			var viewModel = (MainViewModel)DataContext;
			if (File.Exists(filename))
			{
				viewModel.Words = new ObservableCollection<Word>(JsonConvert.DeserializeObject<Word[]>(File.ReadAllText(filename)).OrderBy(t => t.Category).OrderBy(t => t.Meaning).OrderBy(t => t.Name));
			}
		}

		private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var viewModel = (MainViewModel)DataContext;
			viewModel.EditWord(this);
		}
	}
}
