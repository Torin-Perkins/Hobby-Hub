using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest.Helpers;
using XamarinTest.Models;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        IAuth auth;
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        int homeid = (int)MenuItemType.Browse;

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
                MainPage.UserID = await auth.GetUserID(EmailInput.Text, PasswordInput.Text);
                await firestoreHelper.UpDateUserLog(MainPage.UserID, true);
                await RootPage.NavigateFromMenu(homeid);
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
