using System;
using Xamarin.Forms.CommonCore;

namespace sqliteDemo
{
    public class Person : CoreSqlModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
