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
    public partial class ArtPage : ContentPage
    {
        ItemsViewModel viewModel;
        List<Hobby> minItemList;
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public ArtPage()
        {
            InitializeComponent();
            /*
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Painting", Description = "This is an art item description.", ParentCategory = "Art" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Stained Glass", Description = "This is an item description.", ParentCategory = "Art" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Pottery", Description = "This is an item description.", ParentCategory = "Art" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Drawing", Description = "This is an item description.", ParentCategory = "Art" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "IRL Art", Description = "This is an item description.", ParentCategory = "Art" });
            firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Tiles", Description = "This is an item description.", ParentCategory = "Art" });
             minItemList = new List<Hobby>()
             {
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Painting", Description="This is an art item description.", ParentCategory = "Art" },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Stained Glass", Description="This is an item description.", ParentCategory = "Art"  },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Pottery", Description="This is an item description.", ParentCategory = "Art"  },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Drawing", Description="This is an item description.", ParentCategory = "Art"  },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "IRL Art", Description="This is an item description.", ParentCategory = "Art"  },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Tiles", Description="This is an item description.", ParentCategory = "Art"  }
             };*/
            GetMinList();
            BindingContext = viewModel = new ItemsViewModel("Art", minItemList);

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
            string input = "Art";
            minItemList = await firestoreHelper.GetHobbiesByParent(input);
        }
    }
}