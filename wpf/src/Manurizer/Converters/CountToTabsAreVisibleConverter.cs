using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manurizer.Converters
{
	public class CountToTabsAreVisibleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is int count)
			{
				return count > 1;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
