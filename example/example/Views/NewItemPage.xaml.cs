using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using example.Models;

namespace example.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Note Note { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            // Unix Timestamp to get random images
            String date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
            Note = new Note
            {
                Text = "",
                ImageUrl = "https://picsum.photos/50/50?nocache=" + date
            };
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Note);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}