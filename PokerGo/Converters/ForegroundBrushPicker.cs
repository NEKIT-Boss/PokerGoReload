using System;
using System.Net.NetworkInformation;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace PokerGo.Converters
{
    public class ForegroundBrushPicker : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var backgroundColor = (Color) value;

            double luminance = 1 - (0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B) / 255;
            return luminance < 0.5 ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException("One way only converter!");
        }
    }
}