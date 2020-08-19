using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinTest.Helpers;
using XamarinTest.Models;

namespace XamarinTest.Views
{

	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : MasterDetailPage
	{
		FirestoreHelper firestoreHelper = new FirestoreHelper();
		MainPage RootPage { get => Application.Current.MainPage as MainPage; }
		public static string UserID;
		
		Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
		public MainPage()
		{
			InitializeComponent();

			MasterBehavior = MasterBehavior.Popover;
			MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
			checkUsers();
		}
		public async void checkUsers()
		{
			var query = await firestoreHelper.QueryDeviceModel(DeviceInfo.Model.ToString());

			System.Diagnostics.Debug.WriteLine("yes: " + query);
			if (!query)
			{
				var user = await firestoreHelper.GetUser(DeviceInfo.Model.ToString());
				UserID = user.UserID;
				if (!user.LoggedIn)
				{
					var newPage = new NavigationPage(new LoginPage());

					if (newPage != null && Detail != newPage)
					{
						Detail = newPage;

						if (Device.RuntimePlatform == Device.Android)
							await Task.Delay(100);

						IsPresented = false;
					}
				}
			}
			else
			{

				var newPage = new NavigationPage(new LoginPage());
				if (newPage != null && Detail != newPage)
				{
					Detail = newPage;

					if (Device.RuntimePlatform == Device.Android)
						await Task.Delay(100);

					IsPresented = false;
				}
				// UserID = firestoreHelper.GenerateUserID(DeviceInfo.Model.ToString());
			}
		}

		public async Task NavigateFromMenu(int id)
		{
			if (!MenuPages.ContainsKey(id))
			{
				switch (id)
				{
					case (int)MenuItemType.About:
						MenuPages.Add(id, new NavigationPage(new AboutPage()));
						break;
					case (int)MenuItemType.Browse:
						MenuPages.Add(id, new NavigationPage(new ItemsPage()));
						break;
					case (int)MenuItemType.Science:
						MenuPages.Add(id, new NavigationPage(new SciencePage()));
						break;
					case (int)MenuItemType.Engineering:
						MenuPages.Add(id, new NavigationPage(new EngineeringPage()));
						break;
					case (int)MenuItemType.Technology:
						MenuPages.Add(id, new NavigationPage(new TechnologyPage()));
						break;
					case (int)MenuItemType.Math:
						MenuPages.Add(id, new NavigationPage(new MathPage()));
						break;
					case (int)MenuItemType.Art:
						MenuPages.Add(id, new NavigationPage(new ArtPage()));
						break;
					case (int)MenuItemType.Sports:
						MenuPages.Add(id, new NavigationPage(new SportsPage()));
						break;
					case (int)MenuItemType.LogOut:
						MenuPages.Add(id, new NavigationPage(new LoginPage()));
						break;
				}
			}

			var newPage = MenuPages[id];

			if (newPage != null && Detail != newPage)
			{
				Detail = newPage;

				if (Device.RuntimePlatform == Device.Android)
					await Task.Delay(100);

				IsPresented = false;
			}

		}
	}
}
