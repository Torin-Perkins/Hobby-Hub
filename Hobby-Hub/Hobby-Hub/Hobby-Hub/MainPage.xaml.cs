using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hobby_Hub
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        //This method is accessed when you hit the bottom right button on the keyboard
        //Causes a popup confirming your string was recoreded...
        //...And saves the text you input as a string called "inputText"
        async private void Entry_Completed(object sender, EventArgs e)
        {
            string inputText = ((Entry)sender).Text;
            await DisplayAlert("Your Input has been recorded", "Your input \"" + inputText + "\" has been saved and will be sent to our servers", "OK");
        }
    }
}
