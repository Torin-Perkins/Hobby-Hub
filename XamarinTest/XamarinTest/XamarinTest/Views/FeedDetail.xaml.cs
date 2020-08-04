using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest.Helpers;
using XamarinTest.Models;
using System.Windows;
using Android.Widget;

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

        }

        public async void BtnSend(object sender, EventArgs e)
		{
            if (!IsFormValid()) { await DisplayAlert("Error", "A Message is Required", "OK"); return; }
            firestoreHelper.AddPost(
                new Post
                {
                    PostID = firestoreHelper.GeneratePostID(),
                    PostedByUser = MainPage.UserID,
                    DatePosted = DateTime.Now,
                    PostText = Message.Text + " -User#" + MainPage.UserID,
                    ParentCategory = category,
                    HLocation = "End"
                });
            Message.Text = "";
            updateMessagesAsync();
		}
        private async void updateMessagesAsync()
        {
            if (!(await firestoreHelper.QueryPostByCategory(Title)))
            {
                List<Post> allPosts = await firestoreHelper.getMessages(category);
                foreach (Post post in allPosts)
                {
                    if(post.PostedByUser == MainPage.UserID)
                    {
                        post.HLocation = "End";
                    }
                    else
                    {
                        post.HLocation = "Start";
                    }
                }
                //Msg.ItemsSource = allPosts;

                ItemsCollectionView.ItemsSource = allPosts;
            }
        }
        private bool IsFormValid() => IsNameValid();
        private bool IsNameValid() => !string.IsNullOrWhiteSpace(Message.Text);

        private void ItemsCollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {

        }
    }
}