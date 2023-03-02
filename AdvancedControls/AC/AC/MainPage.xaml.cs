using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials; //引用的类库
using Xamarin.Forms;

namespace AC
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void ClickHyperlinkButton_Tapped(object sender, EventArgs e)
        {
            // 执行点击,跳转浏览器查看网页内容
            await Browser.OpenAsync("https://docs.microsoft.com/zh-cn/");
        }
        private void MyPopupButton_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Greetings!", "You have clicked me!", "OK");
        }
    }
}
