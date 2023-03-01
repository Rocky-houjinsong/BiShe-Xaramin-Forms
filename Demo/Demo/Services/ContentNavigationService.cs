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
       
        /// <summary>
        /// 内容页激活服务.
        /// </summary>
        private IContentPageActivationService _contentPageActivationService;
        //****************继承方法
        /// <summary>
        /// 导航到页面
        /// </summary>
        /// <param name="pageKey">页面键</param>
        /// <returns></returns>
        public async Task NavigateToAsync(string pageKey) =>
            await MainPage.Navigation.PushAsync(_contentPageActivationService.Activate(pageKey));
       // 需要new出一个 参数,进行传值
            //TODO This is a test code
           // await MainPage.Navigation.PushAsync(new AboutPage());自己做判断,,上面是替换方案
        
        //**************公开方法;
        public MainPage MainPage => 
            _mainPage ?? (_mainPage = Application.Current.MainPage as MainPage);
        /// <summary>
        /// 内容导航服务
        /// </summary>
        /// <param name="contentPageActivationService">内容页激活服务</param>
        public ContentNavigationService(IContentPageActivationService contentPageActivationService)
        {
            _contentPageActivationService = contentPageActivationService;
        }
    }
}
