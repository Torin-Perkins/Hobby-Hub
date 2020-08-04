using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTest.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{

		IAuth auth;

		public LoginPage()
		{
			InitializeComponent();
			auth = DependencyService.Get<IAuth>();
		}

		async void LoginSend(object sender, EventArgs e)
		{
			string Token = await auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text);
			if (Token != "")
			{
				//MainPage.UserID = auth.LoginWithEmailPassword(EmailInput.Text, PasswordInput.Text).Id.ToString();
				await Navigation.PushAsync(new ItemsPage());
			}
			else
			{
				ShowError();
			}
		}
		async void RegisterSend(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new RegisterPage());
        }
		async private void ShowError()
		{
				await DisplayAlert("Authentication Failed", "E-mail or password are incorrect. Try again!", "OK");
		}
	}
}
