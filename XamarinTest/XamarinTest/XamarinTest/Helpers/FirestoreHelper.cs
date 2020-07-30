using System;
using System.Collections.Generic;
using System.Text;
using XamarinTest.Models;
using Plugin.CloudFirestore;
using System.Threading.Tasks;
using System.Linq;

namespace XamarinTest.Helpers
{
	/*
	* Class FirestoreHelper
	* Used for connection and CRUD operations within the Google firestore database
	* Uses Firestore.Cloud.Plugin for NoSQL operation
	*/
	class FirestoreHelper
	{
		
		/*
		 * Method to add the model post to the database
		 */
		public async void AddPost(Post post)
		{
			await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Posts").
				AddDocumentAsync(post);
		}
		/*
		 * Adds parameter User to the database
		 */
		public async void CreateNewUser(User user)
		{
			await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Users").
				AddDocumentAsync(user);
		}
		/*
		 * Adds model hobby to the database
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
		 * Queries the Database
		 * Based on the ID number of the User
		 * Returns a bool based on if the query was empty
		 */
		public async Task<bool> QueryUserID(string UserID)
		{
			var document = await CrossCloudFirestore.
				Current.
				Instance.
				GetCollection("Users").
				WhereEqualsTo("UserID", UserID).
				GetDocumentsAsync();

			return document.IsEmpty;
		}
		/*
		 * Queries the Database
		 * Based on the Parent Category of Posts
		 * Return bool based on if the query was empty
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
		 * Queries the Database
		 * Based on the Device Model of the Users
		 * Return bool based on if the query was empty
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
		 * Queries the Database
		 * Based on the ID Number of Posts
		 * Return bool based on if the query was empty
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
		 * Queries the Database
		 * Based on the ID Number of a Hobby
		 * Return bool based on if the query was empty
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
		 * Queries the Database
		 * Based on the Name of A Hobby
		 * Return bool based on if the query was empty
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
		 * Queries the Database
		 * Returns a list of Users based on the model number
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
		 * Queries the Database
		 * Returns a list of Messages based on the category
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
		 * Queries the Database
		 * Returns a list of Hobbies based on the category
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
	}
}
