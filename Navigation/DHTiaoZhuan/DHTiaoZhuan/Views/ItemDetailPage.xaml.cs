using DHTiaoZhuan.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace DHTiaoZhuan.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}