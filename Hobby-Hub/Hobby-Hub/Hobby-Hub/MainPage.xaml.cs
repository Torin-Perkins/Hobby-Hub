﻿using HobbyHub;
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
			
			  Post post = new Post() {ID = "FYTTH561", postID = GeneratePostID(), postedByUser = "1234", date = new Timestamp(), postText = Message.Text,catiegory = "Test"};
			  fs.test(post);

			// fs.test();
			updateMessagesAsync();



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
		   
			System.Diagnostics.Debug.WriteLine(query.ToString());
			if (query.ToString() != "System.Threading.Tasks.Task`1[System.Object]") 
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
			var allPosts = await fs.getMessages("Test");
			Msg.ItemsSource = allPosts;
		}
	}
}
