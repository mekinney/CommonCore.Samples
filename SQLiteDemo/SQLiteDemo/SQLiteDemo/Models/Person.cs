using System;
using SQLite;

namespace SQLiteDemo.Models
{
    public class Person
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [NotNull]
        public string FirstName { get; set; }
        [NotNull]
        public string LastName { get; set; }
        [NotNull]
        public string PhoneNumber { get; set; }
    }
}
