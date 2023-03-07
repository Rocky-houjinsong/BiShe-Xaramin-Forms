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
    /// ItemTappedEventArgsToPoetryConverter 到诗词转换器测试
    /// </summary>
    public class ItemTappedEventArgsToPoetryConverterTest
    {
        [Test]
        public void TestConvertBack()
        {
            var itemTappedEventArgsToPoetryConverter = new ItemTappedEventArgsToPoetryConverter();
            Assert.Catch<DoNotCallMeException>(() =>
                itemTappedEventArgsToPoetryConverter.ConvertBack(null, null, null, null));
        }
        [Test]
        public void TestConvertFailed()
        {
            var itemTappedEventArgsToPoetryConverter = new ItemTappedEventArgsToPoetryConverter();
            Assert.IsNull(
                itemTappedEventArgsToPoetryConverter.Convert(new object(), null, null, null));
            var itemTappedEventArgsToConvert = new ItemTappedEventArgs(new object(), null, -1);  // 是EventArgs,但是传入的不是Poetry而是null
            Assert.IsNull(
                itemTappedEventArgsToPoetryConverter.Convert(itemTappedEventArgsToConvert, null, null, null));
        }
        [Test]
        public void TestConvertSucceeded()
        {
            var itemTappedEventArgsToPoetryConverter = new ItemTappedEventArgsToPoetryConverter();
            var poetryToReturn = new Poetry();
            var itemTappedEventArgsToConvert =
                new ItemTappedEventArgs(null, poetryToReturn, -1);
            Assert.AreSame(poetryToReturn,
                itemTappedEventArgsToPoetryConverter.Convert(itemTappedEventArgsToConvert, null, null, null));
            // AreSame 同一个对象
        }
    }
}
