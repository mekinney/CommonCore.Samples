using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteDemo.Models;
using Xamarin.Forms;

namespace SQLiteDemo.Data
{
    class SQLiteData
    {
        SQLiteConnection dbConn;
        public SQLiteData()
        {
            dbConn = DependencyService.Get <ISQLiteData>().GetConnection();
            // create the table(s)
            dbConn.CreateTable<Person>();
        }
        public List<Person> GetAllPersons()
        {
            return dbConn.Query<Person>("Select * From [Person]");
        }
        public int SavePerson(Person aPerson)
        {
            return dbConn.Insert(aPerson);
        }
        public int DeletePerson(Person aPerson)
        {
            return dbConn.Delete(aPerson);
        }
        public int EditPerson(Person aPerson)
        {
            return dbConn.Update(aPerson);
        }
    }
}
