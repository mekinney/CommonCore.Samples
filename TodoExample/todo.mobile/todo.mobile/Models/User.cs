using System;
using Xamarin.Forms.CommonCore;

namespace todo.mobile
{
    public class User : SqlDataModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public string MetaData { get; set; }
    }
}
