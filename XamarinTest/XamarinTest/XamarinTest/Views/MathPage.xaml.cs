using System;
using System.ComponentModel;
using Xamarin.Forms;
using XamarinTest.Models;
using XamarinTest.ViewModels;

namespace XamarinTest.Views
{

    [DesignTimeVisible(false)]
    public partial class MathPage : ContentPage
    {
        ItemsViewModel viewModel;
        public MathPage()
        {
            InitializeComponent();
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
    }
}