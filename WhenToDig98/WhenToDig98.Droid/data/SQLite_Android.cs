using System;
using Xamarin.Forms;
using System.IO;
using WhenToDig98.Droid.Data;
using WhenToDig98.Data;

[assembly: Dependency(typeof(SQLite_Android))]

namespace WhenToDig98.Droid.Data
{
    class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
        }
        #region ISQLite implementation
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "WTGSQLite.db3";
            // Documents folder
            string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal); 

            var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection 
            return conn;
        }
        #endregion
    }
}

