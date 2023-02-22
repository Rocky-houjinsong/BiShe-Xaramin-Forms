using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebService : ContentPage
    {
        public WebService()
        {
            InitializeComponent();
        }

        private async void CallWebServiceButton_OnClicked(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var response =
                await httpClient.GetAsync("https://v2.jinrishici.com/token");
            var json = await response.Content.ReadAsStringAsync();//Http响应的内容读成字符串
            // ResultLabel.Text = json; 这个不是想要的数据,
            //将json对象转化为 今日诗词Token的实例读取data数据,需要一个包  Newtonsoft.Json
            var token = JsonConvert.DeserializeObject<jinrishiciToken>(json);
            ResultLabel.Text = token.data;
        }
    }


    
    public class jinrishiciToken
    {
        public string status { get; set; }
        public string data { get; set; }
    }

}