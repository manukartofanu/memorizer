using System.Windows;
#if (!DEBUG)
using Squirrel;
using System.Linq;
using System.Threading.Tasks;
#endif

namespace Manurizer.Views
{
	/// <summary>
	/// Interaction logic for UpdateWindow.xaml
	/// </summary>
	public partial class UpdateWindow : Window
	{
		public UpdateWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
#if (!DEBUG)
			bool restartNeeded = false;
			Task.Run(async () =>
			{
				try
				{
					using (var mgr = GetUpdateManager())
					{
						var updateInfo = await mgr.CheckForUpdate();
						if (updateInfo.ReleasesToApply.Any())
						{
							var updateResult = await mgr.UpdateApp();
							restartNeeded = true;
						}
					}
				}
				catch
				{
					Dispatcher.Invoke(() =>
					{
						Close();
					});
				}
				if (restartNeeded)
				{
					Dispatcher.Invoke(() =>
					{
						UpdateManager.RestartApp();
					});
				}
				else
				{
					Dispatcher.Invoke(() =>
					{
						Close();
					});
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
	}
}
