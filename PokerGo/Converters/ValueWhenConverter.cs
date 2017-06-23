using System;
using Windows.UI.Xaml.Data;

namespace PokerGo.Converters
{
    public class ValueWhenConverter : IValueConverter
    {
        public object Value { get; set; }
        public object Otherwise { get; set; }
        public object When { get; set; }
        public object OtherwiseValueBack { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                return Equals(value, parameter ?? When) ? Value : Otherwise;
            }

            catch
            {
                return Otherwise;
            }
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)

        {
            if (OtherwiseValueBack == null)
                throw new InvalidOperationException("Cannot ConvertBack if no OtherwiseValueBack is set!");

            try
            {
                return Equals(value, Value) ? When : OtherwiseValueBack;
            }

            catch
            {
                return OtherwiseValueBack;
            }
        }
    }
}