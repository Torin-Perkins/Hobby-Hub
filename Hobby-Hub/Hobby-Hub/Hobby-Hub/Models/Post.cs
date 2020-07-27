using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HobbyHub.Models
{
    class Post { 
        [Id]
        public string ID { get; set; }
        public string postID { get; set; }
        public string postedByUser { get; set; }

        public DateTime date { get; set; }
        public string postText { get; set; }
        public string catiegory { get; set; }
}
}
