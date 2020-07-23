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
    public partial class WelcomePage: ContentPage
    {
        private string email;

        WelcomVM welcomePageVM;
        public WelcomePage(string email)
        {
            InitializeComponent();
            welcomePageVM = new WelcomVM(email);
            BindingContext = welcomePageVM;
        }
    }
}