using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MD
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Preference : ContentPage
    {
        public Preference()
        {
            InitializeComponent();
        }

        private void PreferenceSaveButton_OnClicked(object sender, EventArgs e)
        {
            Xamarin.Essentials.Preferences.Set("Key", PreferenceEntry.Text);
        }

        private void PreferenceReadButton_OnClicked(object sender, EventArgs e)
        {
            PreferenceResultLabel.Text =
                Xamarin.Essentials.Preferences.Get("Key", "No value");
        }
    }
}