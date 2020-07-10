using Manurizer.Views;
using Squirrel;
using System.Windows;

namespace ManurizerWpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private async void Application_Startup(object sender, StartupEventArgs e)
		{
			using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/manukartofanu/memorizer"))
			{
				await mgr.Result.UpdateApp();
			}
			MainWindow window = new MainWindow();
			window.Show();
		}
	}
}
