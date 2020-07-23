using HobbyHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HobbyHub
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class LoginPage : ContentPage
{
    LoginViewModel loginViewModel;
    public LoginPage()
    {
            loginViewModel = new LoginViewModel();
            InitializeComponent();
            BindingContext = loginViewModel;
        }
        private void Loginbtn_Clicked(object sender, EventArgs e)
        {
            //null or empty field validation, check weather email and password is null or empty  
            if (string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text))
                DisplayAlert("Empty Values", "Please enter Email and Password", "OK");
            else
            {
                if (Email.Text == "abc@gmail.com" && Password.Text == "1234")
                {
                    DisplayAlert("Login Success", "", "Ok");
                    //Navigate to Wellcom page after successfully login  
                    Navigation.PushAsync(new WelcomePage(Email.Text));
                }
                else
                    DisplayAlert("Login Fail", "Please enter correct Email and Password", "OK");
            }
        }

        private void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}