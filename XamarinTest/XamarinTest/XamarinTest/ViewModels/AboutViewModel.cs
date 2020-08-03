using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinTest.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenNwapwCommand = new Command(async () => await Browser.OpenAsync("http://nwapw.org/"));
            OpenXamCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
            OpenGitCommand = new Command(async () => await Browser.OpenAsync("https://github.com/Torin-Perkins/Hobby-Hub"));
        }

        public ICommand OpenNwapwCommand { get; }
        public ICommand OpenXamCommand { get; }
        public ICommand OpenGitCommand { get; }
    }
}