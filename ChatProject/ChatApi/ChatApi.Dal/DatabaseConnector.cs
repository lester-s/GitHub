using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Models;
using SQLite;

namespace ChatApi.Dal
{
    public class DatabaseConnector
    {
        private static SQLiteConnection _dbConnector;

        public static SQLiteConnection DBConector
        {
            get 
            {
                if (_dbConnector == null)
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    _dbConnector = new SQLiteConnection(path + @"\chatDB");
                    _dbConnector.CreateTable<User>();
                }
                return _dbConnector;
            }
        }
        
    }
}
