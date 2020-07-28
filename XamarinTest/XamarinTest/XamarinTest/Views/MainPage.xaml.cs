using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinTest.Models;

namespace XamarinTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    // TODO: change this back to about page
                    // TODO: the default page should be about page not browse page
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