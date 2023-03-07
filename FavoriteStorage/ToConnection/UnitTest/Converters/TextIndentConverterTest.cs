using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ToConnection.Converters;
using ToConnection.Utils;

namespace ValueConverter.UnitTest.Converters
{
    /// <summary>
    /// 文本缩进转换器测试
    /// </summary>
    public class TextIndentConverterTest
    {
        [Test]
        public void TestConvertBack()
        {
            // 调用抛出异常就是 成功
            var textIndentConverter = new TextIndentConverter();
            Assert.Catch<DoNotCallMeException>(() =>
                textIndentConverter.ConvertBack(null, null, null, null
                ));
        }
        // 转换失败的情况 传 非字符串的情况,就应该返回为Null
        [Test]
        public void TestConvertFailed()
        {
            var textIndentConverter = new TextIndentConverter();
            Assert.IsNull(textIndentConverter.Convert(new object(), null, null, null));
        }
        [Test]
        public void TestConvertSucceeded()
        {
            var textIndentConverter = new TextIndentConverter();
            var stringToConvert = "A\nB\nC";
            var stringConverted =
                textIndentConverter.Convert(stringToConvert, null, null, null) as string;
            Assert.AreEqual("\u3000\u3000A\n\u3000\u3000B\n\u3000\u3000C", stringConverted);
        }
    }
}
