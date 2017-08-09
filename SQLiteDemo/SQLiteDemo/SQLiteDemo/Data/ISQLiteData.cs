using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SQLiteDemo.Data
{
    interface ISQLiteData
    {
        SQLiteConnection GetConnection();
    }
}
