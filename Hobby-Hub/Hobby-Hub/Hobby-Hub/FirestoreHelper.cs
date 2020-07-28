using HobbyHub.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHub
{
    class FirestoreHelper
{   
        public async void test(Post post)
        {
            await CrossCloudFirestore.Current
                           .Instance
                           .GetCollection("Posts")
                           .AddDocumentAsync(post);
        }
        public async Task<bool> queryPID(string poID)
        {
            var document = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Posts")
                                     .WhereEqualsTo("postID", poID).LimitTo(1)
                                     .GetDocumentsAsync();
            
            return document.IsEmpty;
        }
        public async Task<bool> queryUsers(string userID)
        {
            var document = await CrossCloudFirestore.Current.
                Instance.
                GetCollection("Users").
                WhereEqualsTo("UID", userID).
                LimitTo(1).GetDocumentsAsync();
            return document.IsEmpty;
        }
        public async Task<bool> queryDeviceName(string phoneName)
        {
            var document = await CrossCloudFirestore.Current.
                Instance.
                GetCollection("Users").
                WhereEqualsTo("PhoneName", phoneName).
                GetDocumentsAsync();
            return document.IsEmpty;
        }
        public async Task<List<Post>> getMessages(string category)
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Posts")
                                     .WhereEqualsTo("category", category)
                                     .GetDocumentsAsync();
            System.Diagnostics.Debug.WriteLine("POSTS: " + query.ToObjects<Post>().ToList<Post>().First().postText);
            List<Post> posts = query.ToObjects<Post>().ToList<Post>();
            posts.Sort((x, y) => DateTime.Compare(x.date, y.date));
            
            
           
            return posts;
        }
        private bool temp;
        private async void checkUser(string userID)
        {
            temp = await queryUsers(userID);
        }
        public string generateUID(string phoneName)
        {
            Random rnd = new Random();
            int[] numbers = new int[6];
            string userID = "";

            for (int i = 0; i < 6; i++)
            {
                userID += rnd.Next(10).ToString();
            }
            checkUser(userID);
            if (!temp)
            {

                return generateUID(phoneName);
            }
            else
            {
                createNewUser(userID, phoneName);
                return userID;
            }
        }
        public async Task<Person>GetPerson(string deviceName)
        {
            var query = await CrossCloudFirestore.Current.Instance.
                GetCollection("Users").
                WhereEqualsTo("PhoneName", deviceName).LimitTo(1).
                GetDocumentsAsync();
             Person[]persons = query.ToObjects<Person>().ToArray();
            return persons[0];
        }
        public async void createNewUser(string userID, string deviceName)
        {
            await CrossCloudFirestore.Current.Instance.GetCollection("Users").AddDocumentAsync(new Person { UID = userID, PhoneName = deviceName });
        }


    }
}
