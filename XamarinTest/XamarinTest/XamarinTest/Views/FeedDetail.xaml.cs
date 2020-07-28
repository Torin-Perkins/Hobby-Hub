using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedDetail : ContentPage
    {
        public FeedDetail()
        {
            InitializeComponent();
        }

        public FeedDetail(String pageName)
        {
            
            InitializeComponent();
            Title = pageName;
        }

        public async void BtnSend(object sender, EventArgs e)
		{
			// finish this method


		}
	}
}