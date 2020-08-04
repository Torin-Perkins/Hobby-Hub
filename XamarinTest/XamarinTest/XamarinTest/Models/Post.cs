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
    }
}
