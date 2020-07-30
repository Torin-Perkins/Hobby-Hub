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
    public partial class MathPage : ContentPage
    {
        ItemsViewModel viewModel;
        List<Hobby> minItemList;
        FirestoreHelper firestoreHelper = new FirestoreHelper();
       
        public MathPage()
        {
            InitializeComponent();
            /* firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Algebra", Description = "This is a math item description.", ParentCategory = "Math" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Geometry", Description = "This is an item description.", ParentCategory = "Math" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Differntial Calculus", Description = "This is an item description.", ParentCategory = "Math" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Itegral Calculus", Description = "This is an item description.", ParentCategory = "Math" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Linear Algebra", Description = "This is an item description.", ParentCategory = "Math" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Trigonometry", Description = "This is an item description.", ParentCategory = "Math" });
                minItemList = new List<Hobby>()
                {
                    new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Math item", Description="This is a math item description.", ParentCategory = "Math"  },
                    new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Second item", Description="This is an item description.", ParentCategory = "Math"   },
                    new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Third item", Description="This is an item description.", ParentCategory = "Math"   },
                    new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Fourth item", Description="This is an item description.", ParentCategory = "Math"   },
                    new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Fifth item", Description="This is an item description.", ParentCategory = "Math"   },
                    new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Sixth item", Description="This is an item description.", ParentCategory = "Math"   }
                };*/
          //  GetMinList();
            BindingContext = viewModel = new ItemsViewModel("Math");
            
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
            string input = "Math";
            minItemList = await firestoreHelper.GetHobbiesByParent(input);
        }
    }
}