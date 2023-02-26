using System;
using System.Collections.Generic;
using System.Text;
using Demo.Models;
using Demo.Services;
using GalaSoft.MvvmLight;
using Xamarin.Forms.Extended;

namespace Demo.ViewModels
{
    /// <summary>
    /// 搜索结果页ViewModel.
    /// </summary>
   public class ResultPageViewModel :ViewModelBase
    {
        //*****构造函数
        /// <summary>
        /// 搜索结果页ViewModel.
        /// </summary>
        /// <param name="poetryStorage">诗词存储</param>
        public ResultPageViewModel(IPoetryStorage poetryStorage)
        {

        }
        //******绑定属性
        /// <summary>
        /// 诗词集合.
        /// </summary>
        public InfiniteScrollCollection<Poetry> PoetryCollection { get; }
        /// <summary>
        /// 加载状态.
        /// </summary>
        public string Status
        {
            get => _status;
            set => Set(nameof(Status), ref _status, value);
        }
        /// <summary>
        /// 加载状态.
        /// </summary>
        private string _status;


        //******** 公开变量
        /// <summary>
        /// 一次显示的诗词数量.
        /// </summary>
        public const int PageSize = 20;
        /// <summary>
        /// 正在载入
        /// </summary>
        public const string Loading = "正在载入";
        /// <summary>
        /// 没有满足条件的结果
        /// </summary>
        public const string NoResult = "没有满足条件的结果";
        /// <summary>
        /// 没有更多结果
        /// </summary>
        public const string NoMoreResult = "没有更多结果";
    }
}
