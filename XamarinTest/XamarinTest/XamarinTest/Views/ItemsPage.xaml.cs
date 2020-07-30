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
using System.Diagnostics;

namespace XamarinTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        int artid = (int)MenuItemType.Art;
        int engineeringid = (int)MenuItemType.Engineering;
        int mathid = (int)MenuItemType.Math;
        int scienceid = (int)MenuItemType.Science;
        int sportsid = (int)MenuItemType.Sports;
        int techid = (int)MenuItemType.Technology;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }
        async void OnArtClicked(object sender, EventArgs args)
        {
            await RootPage.NavigateFromMenu(artid);
        }
        async void OnEngineeringClicked(object sender, EventArgs args)
        {
            await RootPage.NavigateFromMenu(engineeringid);
        }
        async void OnMathClicked(object sender, EventArgs args)
        {
            await RootPage.NavigateFromMenu(mathid);
        }
        async void OnScienceClicked(object sender, EventArgs args)
        {
            await RootPage.NavigateFromMenu(scienceid);
        }
        async void OnSportsClicked(object sender, EventArgs args)
        {
            await RootPage.NavigateFromMenu(sportsid);
        }
        async void OnTechClicked(object sender, EventArgs args)
        {
            await RootPage.NavigateFromMenu(techid);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.IsBusy = true;
        }
    }
}