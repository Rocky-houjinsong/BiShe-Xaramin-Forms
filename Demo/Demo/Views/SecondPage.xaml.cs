using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : ContentPage
    {
        public SecondPage()
        {
            InitializeComponent();

            SizeChanged += (sender, args) =>
                VisualStateManager.GoToState(BoxStackLayout, Width > Height ? "Landscape" : "Portarit");
            // 语言组织太啰嗦,直接写
            //List<Poetry> poetryList = new List<Poetry>();  
            //Poetry p;
            //p = new Poetry();
            //p.Name = "Name 1";
            //p.Content = "Content 1";
            //poetryList.Add(p);
            var poetryList = new List<Poetry>
            {
                new Poetry {Name = "Name 1", Content = "Content 1"},
                new Poetry {Name = "Name 2", Content = "Content 2"},
                new Poetry {Name = "Name 3", Content = "Content 3"},
            };

            PoetryListView.ItemsSource = poetryList;
        }

        private void PoetryListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            DisplayAlert("", ((Poetry)e.Item).Name, "OK");
            //输出Debug测试
            Debug.WriteLine(((Poetry)e.Item).Name);
        }
    }
    /// <summary>
    /// 诗,有作者和内容两个属性
    /// </summary>
    public class Poetry
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}