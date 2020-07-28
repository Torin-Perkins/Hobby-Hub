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

namespace XamarinTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SportsPage : ContentPage
    {
        ItemsViewModel viewModel;
        List<Item> minItemList;
        public SportsPage()
        {
            InitializeComponent();

            minItemList = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sports item", Description="This is a sports item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
            BindingContext = viewModel = new ItemsViewModel("Sports", minItemList);

        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
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
    }
}