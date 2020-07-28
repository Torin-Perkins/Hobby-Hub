using HobbyHub;
using HobbyHub.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
			updateMessagesAsync();
			//user = fs.generateUID();
			InitializeComponent();
		}

        public async void BtnSend(object sender, EventArgs e)
        {
			   if (!IsFormValid())
			   {
				   await DisplayAlert("Error", "A Message is Required", "OK");
				  return;
			  }
			//GeneratePostID();
			  Post post = new Post() {ID = "FYTTH561", postID = GeneratePostID(), postedByUser = App.userID, date =  DateTime.Now, postText = Message.Text + " -User#" + App.userID,category = "Test"};
			  fs.test(post);

			// fs.test();
			updateMessagesAsync();



		}
		private bool IsFormValid() => IsNameValid();

		private bool IsNameValid() => !string.IsNullOrWhiteSpace(Message.Text);

		private bool temp;
		private async void checkPID(String poID)
        {
			temp = await fs.queryPID(poID);
        }
		private string GeneratePostID()
		{
			Random rnd = new Random();
			int[] numbers = new int[11];
			string poID = "";

			for (int i = 0; i < 11; i++)
			{
				poID += rnd.Next(10).ToString();
			}
			checkPID(poID);
		   
			System.Diagnostics.Debug.WriteLine(temp);
			if (temp)
			{ 
				return GeneratePostID();
			}
			else
            {
				return poID;
			}

        }
		
		private async void updateMessagesAsync()
		{
			List<Post> allPosts = await fs.getMessages("Test");
			System.Diagnostics.Debug.WriteLine(allPosts.First().ToString());
			Msg.ItemsSource = allPosts;
		}
		
	}
}
