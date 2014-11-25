using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDP.DAL;
using CDP.Models;
using HashMe;

namespace CDP.BLL
{
    public class UserService
    {
        private static UserService _instance;
        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }
        }

        public DBopMessage CreateOneUser(User newUser)
        {
            newUser.FirstName = newUser.FirstName.ToLower();
            newUser.LastName = newUser.LastName.ToUpper();
            newUser.Login = HashMan.GetMD5Hash(newUser.Login);
            newUser.Password = HashMan.GetMD5Hash(newUser.Password);
            newUser.Level = CDPEnum.UserLevel.normal;
            var opStatus = new UserDAL().CreateOneUser(newUser);
            return opStatus;
        }

        public DBopMessage<List<User>> GetAllUsers()
        {
            var users = new UserDAL().GetAllUsers();

            return users;
        }

        public DBopMessage UpdateOneUser(User[] UserData)
        {
            for (int i = 0; i < 2; i++)
            {
                UserData[i].FirstName = UserData[i].FirstName.ToLower();
                UserData[i].LastName = UserData[i].LastName.ToUpper();
                UserData[i].Login = HashMan.GetMD5Hash(UserData[i].Login);
                UserData[i].Password = HashMan.GetMD5Hash(UserData[i].Password);
            }

            var opStatus = new UserDAL().UpdateOneUser(UserData);
            return opStatus;
        }

        public DBopMessage DeleteAllUsers()
        {
            var opStatus = new UserDAL().DeleteAllUsers();
            return opStatus;
        }

        public DBopMessage ConnectUser(string[] credentials)
        {
            var login = credentials[0];
            var pwd = credentials[1];

            var opStatus = new UserDAL().ConnectUser(login, pwd);

            return opStatus;
        }

        public bool AuthorizeUser(IEnumerable<string> creds, CDPEnum.UserLevel level)
        {
           var myCreds = creds.ElementAt(0).Split(';');
            var login = myCreds[0];
            var pwd = myCreds[1];

            var opStatus = new UserDAL().AuthorizeUser(login, pwd, level);

            return opStatus;
        }
    }
}
