﻿using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using XamarinTest.Models;

namespace XamarinTest.Helpers
{
	/*
	 * FirestoreHelper contains every method to access the database
	 * Uses functions in the Plugin.Cloud.Firestore
	 * Simple NoSQL commands to query, retrieve, and update multiple collections
	 * Instantiated by pages that need to access data from Google Firestore Database
	 */
	class FirestoreHelper
	{
		public FirestoreHelper()
		{
		}
		/*
		 * Async method to add model Post to the datbase
		 */
		public async void AddPost(Post post)
		{
			await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Posts").
				AddDocumentAsync(post);
			/*
			 * Async method to add model User to the datbase
			 */
		}
		public async void CreateNewUser(User user)
		{
			await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Users").
				AddDocumentAsync(user);
		}
		/*
		 * Async method to add model Hobby to the datbase
		 */
		public async void CreateNewHobby(Hobby hobby)
		{
			await CrossCloudFirestore.
			   Current.
			   Instance.
			   GetCollection("Hobbies").
			   AddDocumentAsync(hobby);
		}
		/*
		 * Async Task to retrieve a bool datatype
		 * Checks if any Id numbers match the input
		 */
		public async Task<bool> QueryUserID(string UserID)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Users").
				WhereEqualsTo("Id", UserID).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Async Task to retrieve a bool datatype
		 * Checks if any Usernames match the input
		 */
		public async Task<bool> QueryUserByUserName(string UserName)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Users").
				WhereEqualsTo("UserName", UserName).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Async Task to retrieve a bool datatype
		 * Checks if there are any posts in a certain category
		 */
		public async Task<bool> QueryPostByCategory(string ParentCategory)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Posts").
				WhereEqualsTo("ParentCategory", ParentCategory).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Async Task to retrieve a bool datatype
		 * Checks if any Device Models match the input
		 */
		public async Task<bool> QueryDeviceModel(string DeviceModel)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Users").
				WhereEqualsTo("DeviceModel", DeviceModel).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Async Task to retrieve a bool datatype
		 * Checks if any Post ID numbers match the input
		 */
		public async Task<bool> QueryPostID(string PostID)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Posts").
				WhereEqualsTo("PostID", PostID).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Async Task to retrieve a bool datatype
		 * Checks if any hobby ID numbers match the input
		 */
		public async Task<bool> QueryHobbyID(string HobbyID)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Hobbies").
				WhereEqualsTo("Id", HobbyID).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Async Task to retrieve a bool datatype
		 * Checks if any Hobby Names match the input
		 */
		public async Task<bool> QueryHobbyName(string HobbyName)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Hobbies").
				WhereEqualsTo("Text", HobbyName).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Async Task to retrieve a User Model
		 * Returns one user based on Device Model
		 */
		public async Task<User> GetUser(string DeviceModel)
		{
			var query = await CrossCloudFirestore.Current.Instance.
				GetCollection("Users").
				WhereEqualsTo("DeviceModel", DeviceModel).
				LimitTo(1).
				GetDocumentsAsync();
			User[] user = query.ToObjects<User>().ToArray();
			return user[0];
		}
		/*
		 * Async Task to retrieve a User datatype
		 * Returns 1 user based on the UserID
		 */
		public async Task<User> GetUserById(string UserID)
		{
			var query = await CrossCloudFirestore.Current.Instance.
				GetCollection("Users").
				WhereEqualsTo("UserID", UserID).
				LimitTo(1).
				GetDocumentsAsync();
			User[] user = query.ToObjects<User>().ToArray();
			Debug.WriteLine("USBID: " + user[0].LoggedIn);
			return user[0];
		}
		/*
		 * Async Task to retrieve a List of Posts datatype
		 * Returns list posts in one category
		 */
		public async Task<List<Post>> getMessages(string category)
		{
			var query = await CrossCloudFirestore.Current
									 .Instance
									 .GetCollection("Posts")
									 .WhereEqualsTo("ParentCategory", category)
									 .GetDocumentsAsync();
			System.Diagnostics.Debug.WriteLine("POSTS: " + query.ToObjects<Post>().ToList<Post>().First().PostText);
			List<Post> posts = query.ToObjects<Post>().ToList<Post>();
			posts.Sort((x, y) => DateTime.Compare(x.DatePosted, y.DatePosted));
			return posts;
		}
		/*
		 * Async Task to retrieve a List of Hobbies datatype
		 * Returns list hobbies in one category
		 */
		public async Task<List<Hobby>> GetHobbiesByParent(string ParentCategory)
		{
			var query = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Hobbies").
				WhereEqualsTo("ParentCategory", ParentCategory).
				GetDocumentsAsync();
			List<Hobby> hobbies = query.ToObjects<Hobby>().ToList<Hobby>();
			System.Diagnostics.Debug.WriteLine("Hobbies: " + query.ToObjects<Hobby>().ToList<Hobby>().First().Text);

			return hobbies;
		}
		/*
		 * tempUser, CheckUser, and GenerateUserID all work together to Generate a Unique ID
		 * Mixture of async and sync methods for easy access to string
		 */
		private bool tempUser = true;
		private async void checkUser(string UserID)
		{
			tempUser = await QueryUserID(UserID);
		}
		public string GenerateUserID(string DeviceModel)
		{
			Random rnd = new Random();
			int[] numbers = new int[6];
			string userID = "";

			for (int i = 0; i < 6; i++)
			{
				userID += rnd.Next(10).ToString();
			}
			checkUser(userID);
			System.Diagnostics.Debug.WriteLine("TempUser: " + tempUser);

			if (!tempUser)
			{
				return GenerateUserID(DeviceModel);
			}
			else
			{
				CreateNewUser(new User { UserID = userID, DeviceModel = DeviceModel });
				return userID;
			}
		}
		private bool tempPost;
		private async void CheckPostID(String poID)
		{
			tempPost = await QueryPostID(poID);
		}
		public string GeneratePostID()
		{
			Random rnd = new Random();
			int[] numbers = new int[11];
			string poID = "";

			for (int i = 0; i < 11; i++)
			{
				poID += rnd.Next(10).ToString();
			}
			CheckPostID(poID);
			if (tempPost)
			{
				return GeneratePostID();
			}
			else
			{
				return poID;
			}
		}

		private bool tempHobby;
		private async void CheckHobbyID(String poID)
		{
			tempPost = await QueryHobbyID(poID);
		}
		public string GenerateHobbyID()
		{
			Random rnd = new Random();
			int[] numbers = new int[11];
			string hID = "";

			for (int i = 0; i < 11; i++)
			{
				hID += rnd.Next(10).ToString();
			}
			CheckPostID(hID);
			if (tempHobby)
			{
				return GenerateHobbyID();
			}
			else
			{
				return hID;
			}

		}
		public async Task UpDateUserLog(string UserID, bool LoggedIn)
		{
			var query = await CrossCloudFirestore.Current.Instance.
				GetCollection("Users").
				WhereEqualsTo("UserID", UserID).
				LimitTo(1).
				GetDocumentsAsync();
			var yourModel = await GetUserById(UserID);
			var reference =
			CrossCloudFirestore.
				Current.Instance.GetCollection("Users").GetDocument(query.ToString());
			reference = CrossCloudFirestore.Current.Instance.GetDocument($"{"Users"}/{yourModel.Id}");
				   


			await CrossCloudFirestore.Current.Instance.RunTransactionAsync((transaction) =>
			{
				
				
				yourModel.LoggedIn = LoggedIn;

				System.Diagnostics.Debug.WriteLine("Transaction: " + yourModel.LoggedIn);
				
				transaction.UpdateData(reference, yourModel);
			});
		}
	}
}
