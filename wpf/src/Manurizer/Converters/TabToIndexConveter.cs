using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Manurizer.Converters
{
	public class TabToIndexConveter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is TabItem tabItem)
			{
				return ItemsControl.ItemsControlFromItemContainer(tabItem).ItemContainerGenerator.IndexFromContainer(tabItem) + 1;
			}
			return 0;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
