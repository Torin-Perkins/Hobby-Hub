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
    public partial class SignUp : ContentPage
    {
        SignUpVm signUpVM;
        public SignUp()
        {
            InitializeComponent();
            signUpVM = new SignUpVm();
            //set binding    
            BindingContext = signUpVM;
        }
    }
}