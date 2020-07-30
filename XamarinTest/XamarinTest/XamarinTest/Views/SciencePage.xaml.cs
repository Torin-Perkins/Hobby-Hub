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
    public partial class SciencePage : ContentPage
    {
        ItemsViewModel viewModel;
        List<Hobby> minItemList;
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public SciencePage()
        {
            InitializeComponent();
            /* firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Chemistry", Description = "This is a science item description.", ParentCategory = "Science" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Physics", Description = "This is an item description.", ParentCategory = "Science" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Biology", Description = "This is an item description.", ParentCategory = "Science" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Psychology/Sociology", Description = "This is an item description.", ParentCategory = "Science" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Astronomy", Description = "This is an item description.", ParentCategory = "Science" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Geology", Description = "This is an item description.", ParentCategory = "Science" });
             minItemList = new List<Hobby>()
             {
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Chemistry", Description="This is a science item description.", ParentCategory = "Science"  },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Physics", Description="This is an item description." , ParentCategory = "Science" },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Biology", Description="This is an item description.", ParentCategory = "Science"  },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Psychology/Sociology", Description="This is an item description.", ParentCategory = "Science"  },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Astronomy", Description="This is an item description." , ParentCategory = "Science" },
                 new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Geology", Description="This is an item description.", ParentCategory = "Science"  }
             };*/
         //   GetMinList();
            BindingContext = viewModel = new ItemsViewModel("Science");
            
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
            string input = "Science";
            minItemList = await firestoreHelper.GetHobbiesByParent(input);
        }
    }
}