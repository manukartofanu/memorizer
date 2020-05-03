using Manurizer.Models;
using Manurizer.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
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
			var viewModel = (ViewModel)DataContext;
			var words = new List<Word>();
			foreach (var item in viewModel.Words)
			{
				words.Add(new Word(item));
			}
			File.WriteAllText("save.txt", JsonConvert.SerializeObject(words));
		}
	}
}
