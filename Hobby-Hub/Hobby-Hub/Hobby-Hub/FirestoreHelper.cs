using HobbyHub.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HobbyHub
{
    class FirestoreHelper
{
        public async Task<object> queryPID(string poID)
        {
            return await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Posts")
                                     .WhereEqualsTo("postID", poID)
                                     .GetDocumentsAsync();
        }
        public async Task<List<Post>> getMessages(string catiegory)
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("Posts")
                                     .WhereEqualsTo("Catiegory", catiegory)
                                     .OrderBy("date", true)
                                     .GetDocumentsAsync();
            List<Post> posts = query.ToObjects<Post>().ToList();
            
           
            return posts;
        }
}
}
