using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent(); 
           // MainPage = new MainPage();
          // MainPage = new Preference();  //键值存储展示
          // MainPage = new Database();     //数据库展示
          MainPage = new WebService();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
