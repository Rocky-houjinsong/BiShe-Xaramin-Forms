using System;
using System.Collections.Generic;
using System.ComponentModel;
using ToConnection.Models;
using ToConnection.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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