using System;
using System.Globalization;
using ToConnection.Utils;
using Xamarin.Forms;

namespace ToConnection.Converters
{
    /// <summary>
    /// 文本缩进转换器
    /// </summary>
    public class TextIndentConverter : IValueConverter
    {
        // \u3000是空格
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture) =>
            (value as string)?.Insert(0, "\u3000\u3000")
            .Replace("\n", "\n\u3000\u3000");

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture) =>
            throw new DoNotCallMeException();

    }
}