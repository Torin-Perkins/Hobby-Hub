using Plugin.CloudFirestore.Attributes;
using Plugin.Media.Abstractions;
using System;

namespace XamarinTest.Models
{
    class Post
    {
        public string PostID { get; set; }
        public string PostedByUser { get; set; }
        public DateTime DatePosted { get; set; }
        public string PostText { get; set; }
        public string ParentCategory { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        [Ignored]
        public string File { get; set; }
    }
}
