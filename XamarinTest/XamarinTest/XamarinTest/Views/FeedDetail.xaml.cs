using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest.Helpers;
using XamarinTest.Models;

namespace XamarinTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedDetail : ContentPage
    {
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        string category;
        public FeedDetail()
        {
            InitializeComponent();
            //updateMessagesAsync();
        }
        
        public FeedDetail(String pageName)
        {
            
            InitializeComponent();
            Title = pageName;
            category = pageName;
            updateMessagesAsync();
            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                // If you want to update UI, make sure its on the on the main thread.
                // Otherwise, you can remove the BeginInvokeOnMainThread
                Device.BeginInvokeOnMainThread(() => updateMessagesAsync());
                return true;
            });
            //Command RefreshCommand = new Command();
            Msg.RefreshCommand = new Command(() => {
                //Do your stuff.    
                updateMessagesAsync();
                Msg.IsRefreshing = false;
            });


        }

        public async void BtnSend(object sender, EventArgs e)
		{
            if (!IsFormValid()) { await DisplayAlert("Error", "A Message is Required", "OK"); return; }
            User user = await firestoreHelper.GetUserById(MainPage.UserID);
            firestoreHelper.AddPost(
                new Post
                {
                    PostID = firestoreHelper.GeneratePostID(),
                    PostedByUser = user.UserName,
                    DatePosted = DateTime.Now,
                    PostText = Message.Text + " -" + user.UserName,
                    ParentCategory = category
                });
            Message.Text = "";
            updateMessagesAsync();
		}
        private async void updateMessagesAsync()
        {
            if (!(await firestoreHelper.QueryPostByCategory(Title)))
            {
                List<Post> allPosts = await firestoreHelper.getMessages(category);
                Msg.ItemsSource = allPosts;
            }
        }
        private bool IsFormValid() => IsNameValid();
        private bool IsNameValid() => !string.IsNullOrWhiteSpace(Message.Text);
    }
}