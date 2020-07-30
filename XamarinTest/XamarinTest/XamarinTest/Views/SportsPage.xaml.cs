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
	
	[DesignTimeVisible(false)]
	public partial class SportsPage : ContentPage
	{
		ItemsViewModel viewModel;
	   
		public SportsPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new ItemsViewModel("Sports");

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
		
	}
}