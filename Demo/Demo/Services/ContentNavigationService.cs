using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Views;
using Xamarin.Forms;

namespace Demo.Services
{
    public class ContentNavigationService:IContentNavigationService
    {
        //**************** 私有变量
        private MainPage _mainPage;
        //  Application.Current.MainPage ==>MainPage = new AppShell();
        public MainPage MainPage =>
            _mainPage ?? (_mainPage = Application.Current.MainPage as MainPage);
        //****************继承方法
        /// <summary>
        /// 导航到页面
        /// </summary>
        /// <param name="pageKey"></param>
        /// <returns></returns>
        public Task NavigateToAsync(string pageKey)
        {
            throw new NotImplementedException();
        }
    }
}
