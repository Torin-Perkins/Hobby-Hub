using HobbyHub;
using HobbyHub.Models;
using Plugin.CloudFirestore;
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
        FirestoreHelper fs = new FirestoreHelper();
        public MainPage()
        {
            InitializeComponent();
        }
        public async void BtnSend(object sender, EventArgs e)
        {
            if (!IsFormValid())
            {
                await DisplayAlert("Error", "A Message is Required", "OK");
                return;
            }
            Post post = new Post() { postID = GeneratePostID(), postedByUser = "1234", date = new Timestamp(), postText = Message.Text,catiegory = "Test"};
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Posts")
                         .AddDocumentAsync(post);

            
            
        }
        private bool IsFormValid() => IsNameValid();

        private bool IsNameValid() => !string.IsNullOrWhiteSpace(Message.Text);
        private string GeneratePostID()
        {
            Random rnd = new Random();
            int[] numbers = new int[11];
            string poID = "";

            for (int i = 0; i < 11; i++)
            {
                poID += rnd.Next(10).ToString();
            }
            var query = fs.queryPID(poID);
          //  if(query != null)
           // {
               
              //  return GeneratePostID();
          //  }
           // else
          //  {
                return poID;
           // }


        }
        private async Task updateMessagesAsync()
        {
            var allPosts = await fs.getMessages("Test");
            Msg.ItemsSource = allPosts;
        }
    }
}
