using Manurizer.Core;
using Manurizer.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
#if (!DEBUG)
using Squirrel;
using System.Reflection;
using System.Threading.Tasks;
#endif

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
#if (!DEBUG)
			Task.Run(async () =>
			{
				using (var mgr = GetUpdateManager())
				{
					var updateInfo = await mgr.CheckForUpdate();
					if (updateInfo.ReleasesToApply.Any())
					{
						var updateResult = await mgr.UpdateApp();
						MessageBox.Show($"You use {Assembly.GetExecutingAssembly().GetName().Version} version. Restart the Manurizer to use newer version.");
					}
				}
			});
#endif
		}

#if (!DEBUG)
		private static UpdateManager GetUpdateManager()
		{
			return UpdateManager.GitHubUpdateManager("https://github.com/manukartofanu/memorizer").Result;
		}
#endif

		private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			var viewModel = (MainViewModel)DataContext;
			viewModel.EditWord(this);
		}
	}
}
