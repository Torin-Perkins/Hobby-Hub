using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using XamarinTest.Droid;
using XamarinTest;
using Xamarin.Forms;

//[assembly: Dependency(typeof(AuthDroid))]
[assembly: Xamarin.Forms.Dependency(typeof(AuthDroid))]
namespace XamarinTest.Droid
{
    public class AuthDroid : IAuth
    {
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdToken(false);
                return (string)token;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }

        public async Task<bool> SignUpWithEmailPasswordAsync(string email, string password)
        {
            try
            {
                

                var signUpTask = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);// await FirebaseAuth.Instance.CreateUserWithEmailAndPassword(email, password);
                System.Diagnostics.Debug.WriteLine("SIGN: " + signUpTask != null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<string> GetUserID(string email, string password)
        {
            var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            var token = user.User.Uid;
            return token;
        }
    }
}