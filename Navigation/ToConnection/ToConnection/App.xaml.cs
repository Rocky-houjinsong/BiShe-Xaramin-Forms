using System;
using ToConnection.Services;
using ToConnection.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToConnection
{
    public partial class App : Application
    {
        // UIParent used by Android version of the app
        public static object AuthUIParent;

        // Keychain security group used by iOS version of the app
        public static string iOSKeychainSecurityGroup;

        public App()
        {
            InitializeComponent();

          //  DependencyService.Register<MockDataStore>();
           // MainPage = new AppShell();
           MainPage = new MainPage();
          // MainPage = new NavigationPage(new ResultPage());
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
