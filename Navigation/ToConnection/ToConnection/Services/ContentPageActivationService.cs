using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ToConnection.Services
{
    /// <summary>
    /// 内容页激活服务.
    /// </summary>
    public class ContentPageActivationService : IContentPageActivationService
    {
        //**************** 私有变量;
        /// <summary> 
        /// 页面缓存.
        /// </summary>
        private Dictionary<string, ContentPage> cache =
            new Dictionary<string, ContentPage>();

        //**************** 继承方法
        public ContentPage Activate(string pageKey) =>
            cache.ContainsKey(pageKey)
                ? cache[pageKey]
                : cache[pageKey] =
                    (ContentPage)Activator.CreateInstance(ContentNavigationConstants.PageKeyTypeDictionary[pageKey]);

    }
}
