﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using ToConnection.Services;

namespace ToConnection.ViewModels
{
    /// <summary>
    /// Locate ViewModel,定位ViewModel的作用
    /// 将ViewModel绑定到View的第一块拼图
    /// 借助 ViewModelLocator找 ViewModel实例,如 MainPageViewModel,那该如何找到
    /// ViewModelLocator呢? 在全局资源 app.xaml中,注册ViewModelLocator实例
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// 在Spring中注册Bean
        /// 在这里 注册类
        /// </summary>
        public ViewModelLocator()
        {
            /*SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<AnotherPageViewModel>();
            SimpleIoc.Default.Register<DataBasePageViewModel>();
            SimpleIoc.Default.Register<IFavoriteStorage, FavoriteStorage>();*/
            SimpleIoc.Default.Register<IPoetryStorage, PoetryStorage>();
            SimpleIoc.Default.Register<ResultPageViewModel>();
            SimpleIoc.Default.Register<IPreferenceStorage, PreferenceStorage>();
        }
        // 借助字段MainPageViewModel进行获取该实例中的值.  
        /*public MainPageViewModel MainPageViewModel =>
            SimpleIoc.Default.GetInstance<MainPageViewModel>();

        public AnotherPageViewModel AnotherPageViewModel =>
            SimpleIoc.Default.GetInstance<AnotherPageViewModel>();

        public DataBasePageViewModel DataBasePageViewModel =>
            SimpleIoc.Default.GetInstance<DataBasePageViewModel>();*/
        /// <summary>
        /// 搜索结果页VieModel
        /// </summary>
        public ResultPageViewModel ResultPageViewModel =>
            SimpleIoc.Default.GetInstance<ResultPageViewModel>();
    }
}
