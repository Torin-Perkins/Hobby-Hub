using Plugin.CloudFirestore.Attributes;

namespace XamarinTest.Models
{
    class User
    {
        [Id] public string Id { get; set; }
        public string UserID { get; set; }
        public string DeviceModel { get; set; }
        public string UserName { get; set; }
        public bool LoggedIn { get; set; }

       
    }
}
