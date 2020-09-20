using Manurizer.Views;
using System.Windows;

namespace ManurizerWpf
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
#if (!DEBUG)
			new UpdateWindow().ShowDialog();
#endif
			new MainWindow().ShowDialog();
		}
	}
}
