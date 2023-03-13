using System;
using System.Globalization;
using ToConnection.Models;
using ToConnection.Utils;
using Xamarin.Forms;

namespace ToConnection.Converters
{
    /// <summary>
    /// ItemTappedEventArgs到诗词转换器.
    /// </summary>
    public class ItemTappedEventArgsToPoetryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (value as ItemTappedEventArgs)?.Item as Poetry;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new DoNotCallMeException();
    }
}