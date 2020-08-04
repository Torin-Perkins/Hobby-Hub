using XamarinTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        ViewCell lastCell;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Browse, Title="Home", ImagePath="Home.png" },
                new HomeMenuItem {Id = MenuItemType.Art, Title="Art", ImagePath="Art.jpg" },
                new HomeMenuItem {Id = MenuItemType.Engineering, Title="Engineering", ImagePath="Engineering.png" },
                new HomeMenuItem {Id = MenuItemType.Math, Title="Math", ImagePath="Math.jpg" },
                new HomeMenuItem {Id = MenuItemType.Science, Title="Science", ImagePath="Science.png" },
                new HomeMenuItem {Id = MenuItemType.Sports, Title="Sports", ImagePath="Sports.jpg" },
                new HomeMenuItem {Id = MenuItemType.Technology, Title="Technology", ImagePath="Technology.png" },
                new HomeMenuItem {Id = MenuItemType.About, Title="About", ImagePath="HobbyHubIcon.png" }

            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            if (lastCell != null)
                lastCell.View.BackgroundColor = Color.Transparent;

            var viewCell = (ViewCell)sender;
            if(viewCell.View != null)
            {
                viewCell.View.BackgroundColor = Color.FromHex("#AC4FBD");
                lastCell = viewCell;
            }
        }
    }
}
