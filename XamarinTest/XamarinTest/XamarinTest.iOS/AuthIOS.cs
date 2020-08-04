using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Foundation;
using UIKit;

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

        Task<string> IAuth.GetUserID(string email, string password)
        {
            throw new NotImplementedException();
        }

        Task<string> IAuth.LoginWithEmailPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        Task<bool> IAuth.SignUpWithEmailPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}