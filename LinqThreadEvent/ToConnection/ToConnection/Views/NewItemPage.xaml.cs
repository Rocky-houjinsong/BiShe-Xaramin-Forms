using ToConnection.Models;
using ToConnection.ViewModels;
using Xamarin.Forms;


namespace ToConnection.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}