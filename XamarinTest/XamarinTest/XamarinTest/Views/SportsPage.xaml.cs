using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinTest.Models;
using XamarinTest.Views;
using XamarinTest.ViewModels;
using XamarinTest.Helpers;

namespace XamarinTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SportsPage : ContentPage
    {
        ItemsViewModel viewModel;
        List<Hobby> minItemList;
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public SportsPage()
        {
            InitializeComponent();
            /* firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Soccer", Description = "This is a sports item description.", ParentCategory = "Sports" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Football", Description = "This is an item description.", ParentCategory = "Sports" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Basketball", Description = "This is an item description.", ParentCategory = "Sports" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Baseball/Softball", Description = "This is an item description.", ParentCategory = "Sports" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Track/Cross Country", Description = "This is an item description.", ParentCategory = "Sports" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Swimming", Description = "This is an item description.", ParentCategory = "Sports" });

            minItemList = new List<Hobby>()
             {
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Soccer", Description="This is a sports item description.", ParentCategory = "Sports" },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Basket Ball", Description="This is an item description.", ParentCategory = "Sports" },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Third item", Description="This is an item description." , ParentCategory = "Sports"},
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Fourth item", Description="This is an item description." , ParentCategory = "Sports"},
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Fifth item", Description="This is an item description." , ParentCategory = "Sports"},
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Sixth item", Description="This is an item description." , ParentCategory = "Sports"}
             };*/
            GetMinList();
            BindingContext = viewModel = new ItemsViewModel("Sports", minItemList);

        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Hobby)layout.BindingContext;
            // NOTE: You can pass an id through this to read in the correct feed on the page
            await Navigation.PushAsync(new FeedDetail(item.Text));
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
        public async void GetMinList()
        {
            string input = "Sports";
            minItemList = await firestoreHelper.GetHobbiesByParent(input);
        }
    }
}