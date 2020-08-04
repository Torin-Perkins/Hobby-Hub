using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinTest
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<bool> SignUpWithEmailPasswordAsync(string email, string password);
        Task<string> GetUserID(string email, string password);
    }
}
