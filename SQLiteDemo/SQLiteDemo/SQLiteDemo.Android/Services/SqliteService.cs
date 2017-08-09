using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteDemo.Data;
using SQLiteDemo.Droid.Services;
using Xamarin.Forms;
using Console = System.Console;
using File = System.IO.File;

[assembly: Dependency(typeof(SqliteService))]
namespace SQLiteDemo.Droid.Services
{
    public class SqliteService : ISQLiteData
    {
        public SqliteService() { }

        #region ISQLite implementation
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "SQLiteDemo.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            Console.WriteLine(path);
            if (!File.Exists(path)) File.Create(path);
            var conn = new SQLiteConnection(path);
            // Return the database connection 
            return conn;
        }

       #endregion


    }
}