using HobbyHub;
using HobbyHub.Models;
using System;
using System.Runtime.ConstrainedExecution;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hobby_Hub
{
    public partial class App : Application
    {
        FirestoreHelper fs = new FirestoreHelper();
        public static string userID;
        public App()
        {
            InitializeComponent();
            checkUsers();
                MainPage = new MainPage();
        }
        public async void checkUsers()
        {
            var query = await fs.queryDeviceName(DeviceInfo.Model.ToString());

            System.Diagnostics.Debug.WriteLine("yes: " + query);
            if (!query)
            {
                Person person = await fs.GetPerson(DeviceInfo.Model.ToString());
                System.Diagnostics.Debug.WriteLine("yes: " + person.UID);

                userID = person.UID;
            }
            else
            {
                userID = fs.generateUID(DeviceInfo.Model.ToString());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
