using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
        bool imgSend = false;
        MediaFile file;
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
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                // If you want to update UI, make sure its on the on the main thread.
                // Otherwise, you can remove the BeginInvokeOnMainThread
                Device.BeginInvokeOnMainThread(() => updateMessagesAsync());
                return true;
            });
            //Command RefreshCommand = new Command();
            //Msg.RefreshCommand = new Command(() =>
          //  {
                //Do your stuff.
           //     updateMessagesAsync();
            //    Msg.IsRefreshing = false;
           // });


        }
        public async void BtnPick(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                if (file == null)
                    return;
                imgSend = true;   
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async void BtnSend(object sender, EventArgs e)
        {
            if (!imgSend)
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
                        ParentCategory = category,
                        Type = "Text"
                    });
                Message.Text = "";
                updateMessagesAsync();
            }
            else
            {
                User user = await firestoreHelper.GetUserById(MainPage.UserID);
                string PiD = firestoreHelper.GeneratePostID();
                try
                {
                    firestoreHelper.AddPost(new Post
                    {
                        PostID = PiD,
                        PostedByUser = user.UserName,
                        DatePosted = DateTime.Now,
                        ParentCategory = category,
                        Type = "Image",
                        FileName = category + "-" + PiD,
                        PostText = " -" + user.UserName
                    });
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                imgSend = false;
                await firestoreHelper.UploadFile(file.GetStream(), category + "-" + PiD, category);
            }
        }
        private async void updateMessagesAsync()
        {
            try
            {
                if (!(await firestoreHelper.QueryPostByCategory(Title)))
                {
                    List<Post> allPosts = await firestoreHelper.getMessages(category);
                    foreach (Post post in allPosts){
                        if (post.FileName != null)
                        {
                            post.File = await firestoreHelper.GetFile(post.FileName, category);
                        }
                    }
                    //Msg.ItemsSource = allPosts;
                    ItemsCollectionView.ItemsSource = allPosts;
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private bool IsFormValid() => IsNameValid();
        private bool IsNameValid() => !string.IsNullOrWhiteSpace(Message.Text);
       
    }
}
