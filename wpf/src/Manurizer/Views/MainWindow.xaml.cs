﻿using Manurizer.Models;
using Manurizer.ViewModels;
using Newtonsoft.Json;
using Squirrel;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
			File.WriteAllText("save.txt", JsonConvert.SerializeObject(viewModel.Words));
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			string filename = "save.txt";
			var viewModel = (MainViewModel)DataContext;
			if (File.Exists(filename))
			{
				viewModel.Words = new ObservableCollection<Word>(JsonConvert.DeserializeObject<Word[]>(File.ReadAllText(filename)).OrderBy(t => t.Category).OrderBy(t => t.Meaning).OrderBy(t => t.Name));
			}
			Task.Run(async () =>
			{
				var mgr = GetUpdateManager();
				await mgr.UpdateApp();
			});
		}

		private static UpdateManager GetUpdateManager()
		{
			return UpdateManager.GitHubUpdateManager("https://github.com/manukartofanu/memorizer").Result;
		}
	}
}
