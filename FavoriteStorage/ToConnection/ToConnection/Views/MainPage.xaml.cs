using System.ComponentModel;
using Xamarin.Forms;


namespace ToConnection.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
#pragma warning disable CS0618 //   “MasterDetailPage”已过时:“MasterDetailPage is obsolete as of version 5.0.0. Please use FlyoutPage instead.”
    public partial class MainPage : MasterDetailPage
#pragma warning restore CS0618 // “MasterDetailPage”已过时:“MasterDetailPage is obsolete as of version 5.0.0. Please use FlyoutPage instead.”
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}