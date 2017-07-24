using System;
using LiteDB;

namespace litedbDemo
{
    public class Person : LiteDbModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
