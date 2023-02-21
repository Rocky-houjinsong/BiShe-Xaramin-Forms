using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void CallWebServiceButton_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}