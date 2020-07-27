using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HobbyHub.Models
{
    class Post
{
        public string postID { get; set; }
        public string postedByUser { get; set; }

        public Timestamp date { get; set; }
        public string postText { get; set; }
        public string catiegory { get; set; }
}
}
