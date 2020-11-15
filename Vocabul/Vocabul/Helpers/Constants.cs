using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vocabul.Classes
{
    class Constants
    {
          public const string DatabaseFilename = "VocalDB.db";

        public const SQLite.SQLiteOpenFlags Flags =
    // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
    // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
    // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = "D:/C#-Projets/Vocabul/Vocabul/Vocabul/DataBase/";
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
