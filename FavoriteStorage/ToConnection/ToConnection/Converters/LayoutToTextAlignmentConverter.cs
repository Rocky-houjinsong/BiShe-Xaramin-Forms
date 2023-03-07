using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ToConnection.Models;
using ToConnection.Utils;
using Xamarin.Forms;

namespace ToConnection.Converters
{
    /// <summary>
    /// 布局 到文本对齐转换器
    /// </summary>
    public class LayoutToTextAlignmentConverter:IValueConverter  /*继承 值转换器*/
    {
        //将值转换成显示
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value as string)
            {
                case Poetry.CenterLayout:
                    return TextAlignment.Center;
                case Poetry.IndentLayout:
                    return TextAlignment.Start;
                default:
                    return null;
            }
        }
        //将显示转换成值 ,项目代码不需要该函数 调用就退出
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new DoNotCallMeException();
        
        

    }
}
