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
		{InitializeComponent();}
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