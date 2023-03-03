using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToConnection.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToConnection.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }
        private static ContentPageActivationService contentPageActivationService =
            new ContentPageActivationService();

        private static ContentNavigationService contentNavigationService =
            new ContentNavigationService(contentPageActivationService);



        private async void Button_OnClicked(object sender, EventArgs e)
        {
            // TODO 测试性质 每次都new ,内存泄漏
            var cns = new ContentNavigationService(new ContentPageActivationService());
            await cns.NavigateToAsync(ContentNavigationConstants.AboutPage);

            // await contentNavigationService.NavigateToAsync(ContentNavigationConstants.AboutPage);
        }
    }
}