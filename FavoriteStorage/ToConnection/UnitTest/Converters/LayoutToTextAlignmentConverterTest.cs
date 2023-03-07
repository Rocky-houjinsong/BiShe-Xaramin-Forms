using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToConnection.Converters;
using ToConnection.Models;
using ToConnection.Utils;
using Xamarin.Forms;

namespace ValueConverter.UnitTest.Converters
{
    /// <summary>
    /// 布局到文本对齐转换器测试.
    /// </summary>
    public class LayoutToTextAlignmentConverterTest
    {
        [Test]
        public void TestConvertBack()
        {
            var layoutToTextAlignmentConverter = 
                new LayoutToTextAlignmentConverter();
            Assert.Catch<DoNotCallMeException>(() =>
                layoutToTextAlignmentConverter.ConvertBack(null, null, null, null));
        }
        [Test]
        public void TestConvert()
        {
            var layoutToTextAlignmentConverter = new LayoutToTextAlignmentConverter();
            Assert.AreEqual(TextAlignment.Center,
                layoutToTextAlignmentConverter.Convert(Poetry.CenterLayout, null, null, null));
            Assert.AreEqual(TextAlignment.Start,
                layoutToTextAlignmentConverter.Convert(Poetry.IndentLayout, null, null, null));
            Assert.AreEqual(null,
                layoutToTextAlignmentConverter.Convert(null, null, null, null));
            Assert.AreEqual(null,
                layoutToTextAlignmentConverter.Convert(string.Empty, null, null, null));
        }
    }
}
