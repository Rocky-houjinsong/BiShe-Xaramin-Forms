using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ClickHyperlinkButton_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync("https://docs.microsoft.com/zh-cn/");


        }

        private void MyPopupButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Greetings!", "You have clicked me!", "OK");
        }
    }
}