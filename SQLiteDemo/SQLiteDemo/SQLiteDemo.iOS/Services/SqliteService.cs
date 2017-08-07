﻿using System;
using System.IO;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using SQLiteDemo.Data;
using SQLiteDemo.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqliteService))]
namespace SQLiteDemo.iOS.Services
{
    public class SqliteService : ISQLiteData
    {
        #region ISQLite implementation
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "SQLiteDemo.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);

            // Return the database connection 
            return conn;
        }
        public ISQLitePlatform GetPlatform()
        {
            return new SQLitePlatformIOS();
        }
        #endregion
    }
}
