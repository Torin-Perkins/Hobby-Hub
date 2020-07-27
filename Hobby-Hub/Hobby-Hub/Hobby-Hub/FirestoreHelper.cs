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
        public async Task<Object> queryPID(string poID)
        {
            var document = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Posts")
                                     .WhereEqualsTo("postID", poID).LimitTo(1)
                                     .GetDocumentsAsync();
            
            return document;
        }
        public async Task<List<Post>> getMessages(string catiegory)
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Posts")
                                     .WhereEqualsTo("catiegory", catiegory)
                                     .GetDocumentsAsync();
            System.Diagnostics.Debug.WriteLine("POSTS: " + query.ToObjects<Post>().ToList<Post>().First().postText);
            List<Post> posts = query.ToObjects<Post>().ToList<Post>();
            posts.Sort((x, y) => DateTime.Compare(x.date, y.date));
            
            
           
            return posts;
        }
}
}
