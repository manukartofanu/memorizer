﻿using Manurizer.Views;
using Squirrel;
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
			MainWindow window = new MainWindow();
			window.Show();
		}
	}
}
