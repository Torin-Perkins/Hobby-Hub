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
using Xamarin.Forms.Internals;

namespace XamarinTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class EngineeringPage : ContentPage
    {
        ItemsViewModel viewModel;
        List<Hobby> minItemList = new List<Hobby>();
        FirestoreHelper firestoreHelper = new FirestoreHelper("Engineering");
        public EngineeringPage()
        {
            InitializeComponent();
            // FirestoreHelper firestoreHelper = new FirestoreHelper();
            /*
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Electrical Engineering", Description = "This is an engineering item description.", ParentCategory = "Engineering" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Chemical Engineering", Description = "This is an item description.", ParentCategory = "Engineering" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Mechanical Engineering", Description = "This is an item description.", ParentCategory = "Engineering" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Civil Engineering", Description = "This is an item description.", ParentCategory = "Engineering" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Automotive Engineering", Description = "This is an item description.", ParentCategory = "Engineering" });
             firestoreHelper.CreateNewHobby(new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Agricultural Engineering", Description = "This is an item description.", ParentCategory = "Engineering" });
 */

            //  firestoreHelper.GetHobbiesByParentVoid("Engineering");
            // GetMinList();
          //  Task.Run(async () => { await firestoreHelper.GetHobbiesByParentVoid("Engineering"); }).Wait();
          //  System.Diagnostics.Debug.WriteLine("MinList: " + firestoreHelper.hobbies.Count);
            /* new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Electrical Engineering", Description="This is an engineering item description.", ParentCategory = "Engineering"},
             new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Chemical Engineering", Description="This is an item description.", ParentCategory = "Engineering" },
             new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Mechanical Engineering", Description="This is an item description." , ParentCategory = "Engineering"},
             new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Civil Engineering", Description="This is an item description.", ParentCategory = "Engineering" },
             new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Automotive Engineering", Description="This is an item description.", ParentCategory = "Engineering" },
             new Hobby { Id = firestoreHelper.GenerateHobbyID(), Text = "Agricultural Engineering", Description="This is an item description.", ParentCategory = "Engineering" }*/


            BindingContext = viewModel = new ItemsViewModel("Engineering");
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
        
        
    }
}