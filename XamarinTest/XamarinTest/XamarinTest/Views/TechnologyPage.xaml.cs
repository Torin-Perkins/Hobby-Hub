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
    public partial class TechnologyPage : ContentPage
    {
        ItemsViewModel viewModel;
        List<Hobby> minItemList;
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public TechnologyPage()
        {
            InitializeComponent();
            /*   firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Software Development", Description = "This is a technology item description.", ParentCategory = "Technology" });
               firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Hardware Development", Description = "This is an item description.", ParentCategory = "Technology" });
               firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Artificial Intelligence", Description = "This is an item description.", ParentCategory = "Technology" });
               firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Robotics", Description = "This is an item description.", ParentCategory = "Technology" });
               firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Medical", Description = "This is an item description.", ParentCategory = "Technology" });
               firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Cyber Security", Description = "This is an item description.", ParentCategory = "Technology" });
                 minItemList = new List<Hobby>()
                 {
                     new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Software Development", Description="This is a technology item description.", ParentCategory = "Technology" },
                     new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Hardware Development", Description="This is an item description.", ParentCategory = "Technology"  },
                     new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Artificial Intelligence", Description="This is an item description.", ParentCategory = "Technology"  },
                     new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Robotics", Description="This is an item description.", ParentCategory = "Technology"  },
                     new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Medical", Description="This is an item description.", ParentCategory = "Technology"  },
                     new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Cyber Security", Description="This is an item description.", ParentCategory = "Technology"  }
                 };*/
           // GetMinList();
            BindingContext = viewModel = new ItemsViewModel("Technology");

        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Hobby)layout.BindingContext;
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
            string input = "Technology";
            minItemList = await firestoreHelper.GetHobbiesByParent(input);
        }
    }
}