using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest.Helpers;
using XamarinTest.Models;
using XamarinTest.Services;
using XamarinTest.Views;

namespace XamarinTest
{
    public partial class App : Application
    {
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public static string UserID;
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }
        public async void checkUsers()
        {
            var query = await firestoreHelper.QueryDeviceModel(DeviceInfo.Model.ToString());

            System.Diagnostics.Debug.WriteLine("yes: " + query);
            if (!query)
            {
                User user = await firestoreHelper.GetUser(DeviceInfo.Model.ToString());
                UserID = user.UserID;
            }
            else
            {
                UserID = firestoreHelper.GenerateUserID(DeviceInfo.Model.ToString());
            }
        }
        protected override void OnStart()
        {
            //checkUsers();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
