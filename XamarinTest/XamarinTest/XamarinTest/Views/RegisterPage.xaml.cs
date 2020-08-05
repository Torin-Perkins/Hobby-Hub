using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest.Helpers;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        IAuth auth;
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public RegisterPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();
            System.Diagnostics.Debug.WriteLine("SYS" + auth.ToString());
        }
        async void SubmitClicked(Object sender, EventArgs e)
        {
            var created = await auth.SignUpWithEmailPasswordAsync(EmailInput.Text, PasswordInput.Text);

            if (created)
            {
                if (!await firestoreHelper.QueryUserByUserName(UserInput.Text))
                {
                    await DisplayAlert("Sign Up Failed", "Username is already taken.", "OK");
                }
                else
                {
                    await DisplayAlert("Success", "Welcome to our system. Log in to have full access", "OK");
                    firestoreHelper.CreateNewUser(new Models.User { UserID = await auth.GetUserID(EmailInput.Text, PasswordInput.Text), DeviceModel = DeviceInfo.Model.ToString(), UserName = UserInput.Text, LoggedIn = false, Id = firestoreHelper.GenerateUserID(DeviceInfo.Model.ToString() )});
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            else
            {
                await DisplayAlert("Sign Up Failed", "Something went wrong. Try again!", "OK");
            }
        }
        public async void LogOutClicked()
        {
            auth.LogOut();
            MainPage.UserID = null;
            await Navigation.PushAsync(new LoginPage());
        }
    }
}