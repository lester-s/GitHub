using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CDP.Models;

namespace CDP.DAL
{
    public class UserDAL
    {
        private CDP_DBContext db;
        public UserDAL()
        {
            db = CDP_DBContext.Instance;
        }

        public DBopMessage CreateOneUser(User newUser)
        {
            try
            {
                var users = db.Users;
                var num = users.Where(u => u.Login == newUser.Login).Count();
                if (num == 0)
                {
                    db.Users.Add(newUser);
                    if (db.SaveChanges() == 1)
                    {
                        return new DBopMessage(HttpStatusCode.Created, "new user created");
                    }
                    else
                    {
                        return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on user creation");
                    }
                }
                else
                {
                    return new DBopMessage(HttpStatusCode.Forbidden, "user login already in use");
                }
            }
            catch (Exception)
            {
                return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on user creation");
            }
        }

        public DBopMessage<List<User>> GetAllUsers()
        {
            try
            {
                var users = db.Users.ToList();
                if (users != null)
                {
                    return new DBopMessage<List<User>>(HttpStatusCode.OK, "request to get users succeed !!!", users);
                }
                else
                {
                    return new DBopMessage<List<User>>(HttpStatusCode.InternalServerError, "No user to get", null);
                }
            }
            catch (Exception)
            {
                return new DBopMessage<List<User>>(HttpStatusCode.InternalServerError, "Something went wrong on getting users", null);
            }
            
        }

        public DBopMessage UpdateOneUser(User[] UserData)
        {
            try
            {
                var beforeData = UserData[0];
                var afterData = UserData[1];
                var user = db.Users.Where(u => u.Login == beforeData.Login).FirstOrDefault();

                if (user != null)
                {
                    user.FirstName = afterData.FirstName;
                    user.LastName = afterData.LastName;
                    user.BirthDate = afterData.BirthDate;
                    user.Login = afterData.Login;
                    user.Password = afterData.Password;
                    if (db.SaveChanges() == 1)
                    {
                        return new DBopMessage(HttpStatusCode.OK, "Update succeed !!!");
                    }
                    else
                    {
                        return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on updating");
                    }
                }
                else
                {
                    return new DBopMessage(HttpStatusCode.Forbidden, "user not found. Cannot update");
                }
            }
            catch (Exception)
            {
                return new DBopMessage(HttpStatusCode.InternalServerError, "Something went wrong on updating");
            }
        }

        public DBopMessage DeleteAllUsers()
        {
            try
            {
                var users = db.Users.ToList();
                db.Users.RemoveRange(users);
                db.SaveChanges();
                if (db.Users.Count() == 0)
                {
                    return new DBopMessage(HttpStatusCode.OK, "All users have been deleted");
                }
                else
                {
                    return new DBopMessage(HttpStatusCode.InternalServerError, "An error occured during the operation");
                }
            }
            catch (Exception)
            {
                return new DBopMessage(HttpStatusCode.InternalServerError, "An error occured during the operation");
            }
        }

        public DBopMessage ConnectUser(string login, string pwd)
        {
            var users = db.Users;
            var user = users.Where(u => (u.Login == login) && (u.Password == pwd)).FirstOrDefault();
            if (user != null)
            {
                return new DBopMessage(HttpStatusCode.OK, "connection succeed");
            }
            else
            {
                return new DBopMessage(HttpStatusCode.Forbidden, "User not found.");
            }
        }

        public bool AuthorizeUser(string login, string pwd, CDPEnum.UserLevel level)
        {
            var users = db.Users;
            var user = users.Where(u => (u.Login == login) && (u.Password == pwd)).FirstOrDefault();
            if (user.Level >= (level))
            {
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
