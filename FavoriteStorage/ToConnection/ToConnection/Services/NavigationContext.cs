﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ToConnection.Services
{
    /// <summary>
    /// 导航上下文.
    /// </summary>
    public static class NavigationContext
    {
        //平台特性
        /// <summary>
        /// 导航参数属性.
        /// </summary>
        public static readonly BindableProperty NavigationParameterProperty =
            BindableProperty.CreateAttached("NavigationParameter",
                typeof(object),typeof(NavigationContext),null,BindingMode.OneWayToSource);
        /// <summary>
        /// 设置导航参数.
        /// </summary>
        /// <param name="bindableObject">需要设置导航参数的对象.</param>
        /// <param name="value">导航参数.</param>
        public static void SetParameter(BindableObject bindableObject, object value) =>
            bindableObject.SetValue(NavigationParameterProperty, value);
    }
}
