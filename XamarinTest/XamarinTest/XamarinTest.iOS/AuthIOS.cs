using Firebase.Auth;
using Foundation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinTest.iOS;

[assembly:Dependency(typeof(AuthIOS))]
namespace XamarinTest.iOS
{
    public class AuthIOS : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                return "";
            }

        }

        async Task<string> IAuth.GetUserID(string email, string password)
        {
            var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
            var token = user.User.Uid;
            return token;
        }

        async Task<bool> IAuth.SignUpWithEmailPasswordAsync(string email, string password)
        {
            try
            {
                var signUpTask = await Auth.DefaultInstance.CreateUserAsync(email, password);
                return signUpTask != null;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public async void LogOut()
        {
            NSError nSError = new NSError();
            object p = Auth.DefaultInstance.SignOut(out nSError);
        }
    }
}