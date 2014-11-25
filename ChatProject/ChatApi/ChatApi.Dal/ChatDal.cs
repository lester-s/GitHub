using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Models;
using SQLite;

namespace ChatApi.Dal
{
    public class ChatDal
    {
        public User CreateUser(User user)
        {
            var _user = new User() { FirstName = user.FirstName, Name = user.Name, Pseudo = user.Pseudo };

            try
            {
                DatabaseConnector.DBConector.Insert(_user);
            }
            catch (Exception)
            {
                return null;
            }
            return _user;
        }

        public User ConnectUser(string pseudo)
        {
            var result = DatabaseConnector.DBConector.Query<User>("select * from User where Pseudo = ?", pseudo);
            var user = result.FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
