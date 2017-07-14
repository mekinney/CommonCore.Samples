using System;
using Realms;

namespace realmDemo.Models
{
    public class Person : RealmObject
    {
        [Realms.PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
