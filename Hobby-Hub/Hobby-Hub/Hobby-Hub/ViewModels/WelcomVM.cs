using Hobby_Hub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace HobbyHub.ViewModels
{
    class WelcomVM
    {
        private string Email;
        private string Password;

        public WelcomVM(string email)
        {
            this.Email = email;
        }
        public WelcomVM(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }

        public Command UpdateCommand

        {
            get { return new Command(Update); }
        }
        private async void Update()
        {
            try
            {
                if (!string.IsNullOrEmpty(Password))
                {
                    var isupdate = await FirebaseHelper.UpdateUser(Email, Password);
                    if (isupdate)
                        await App.Current.MainPage.DisplayAlert("Update Success", "", "Ok");
                    else
                        await App.Current.MainPage.DisplayAlert("Error", "Record not update", "Ok");
                }
                else
                    await App.Current.MainPage.DisplayAlert("Password Require", "Please Enter your password", "Ok");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{e}");
            }
        }
        public Command DeleteCommand

        {
            get { return new Command(Delete); }
        }
        private async void Delete()
        {
            try
            {
                var isdelete = await FirebaseHelper.DeleteUser(Email);
                if (isdelete)
                    await App.Current.MainPage.Navigation.PopAsync();
                else
                    await App.Current.MainPage.DisplayAlert("Error", "Record not delete", "Ok");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error:{e}");
            }
        }
            public Command LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    App.Current.MainPage.Navigation.PopAsync();
                });
            }
        }
    }
    }



