using System;


namespace couchdbDemo
{
    public interface IDbModel
    {
        string Id { get; set; }
    }
    public class Person : IDbModel
    {
        public string Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
