using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace Demo.Services
{
    /// <summary>
    /// 内容导航服务接口.
    /// </summary>
    public interface IContentNavigationService // :INavigationService 国际通用
    {
        /// <summary>
        /// 导航到页面
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <returns></returns>
        Task NavigateToAsync(string pageKey);
    }

    /// <summary>
    /// 内容导航常量.
    /// </summary>
    public static class ContentNavigationConstants
    {
        /// <summary>
        /// 诗词详情页
        /// </summary>
        public const string AboutPage = nameof(Views.AboutPage);
        /// <summary>
        /// 页面键 - 页面类型字典
        /// </summary>
        public static readonly Dictionary<string, Type> PageKeyTypeDictionary =
            new Dictionary<string, Type>{ { AboutPage, typeof(Views.AboutPage)  } };
    }
}
