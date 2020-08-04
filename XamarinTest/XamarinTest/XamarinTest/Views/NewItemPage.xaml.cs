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
        public string CatagoryName { get; private set; }

        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public NewItemPage()
        {
            InitializeComponent();

            Pick.Items.Add("Art");
            Pick.Items.Add("Engineering");
            Pick.Items.Add("Math");
            Pick.Items.Add("Science");
            Pick.Items.Add("Sports");
            Pick.Items.Add("Technology");



            Item = new Hobby
            {
                Text = "",
                Description = "",
                Id = firestoreHelper.GenerateHobbyID(),
                ParentCategory = "The category it is in"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            // MessagingCenter.Send(this, "AddItem", Item);
            Item.ParentCategory = CatagoryName;
            firestoreHelper.CreateNewHobby(Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void Pick_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Pick.SelectedIndex == -1)
            {

            }
            else
            {
                CatagoryName = Pick.Items[Pick.SelectedIndex];


            }
        }
    }
}
