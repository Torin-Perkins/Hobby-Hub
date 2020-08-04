using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinTest.Helpers;
using XamarinTest.Models;

namespace XamarinTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Hobby Item { get; set; }
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public NewItemPage()
        {
            InitializeComponent();

            Item = new Hobby
            {
                Text = "Item name",
                Description = "This is an item description.",
                Id = firestoreHelper.GenerateHobbyID(),
                ParentCategory = "The category it is in"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            // MessagingCenter.Send(this, "AddItem", Item);
            firestoreHelper.CreateNewHobby(Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
