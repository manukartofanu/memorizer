using Manurizer.Events;
using System;
using System.Windows;
using System.Windows.Media.Animation;

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

		private void Window_Closed(object sender, EventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void ButtonExpand_Click(object sender, RoutedEventArgs e)
		{
			bool isExpanded = (bool)buttonExpand.CommandParameter;
			string storyboardName = isExpanded ? "StoryboardConstrictNavigator" : "StoryboardExpandNavigator";
			if (window.FindResource(storyboardName) is Storyboard storyboard)
			{
				storyboard.Begin();
			}
		}
	}
}
