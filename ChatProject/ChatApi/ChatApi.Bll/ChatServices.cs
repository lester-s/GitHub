using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApi.Dal;
using ChatApi.Models;
using System.Timers;

namespace ChatApi.Bll
{
    public class ChatServices
    {
        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }
            set { _users = value; }
        }
        
        private static ChatServices _instance;
        public static ChatServices Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ChatServices();
                }
                return _instance;
            }
        }

        Action check;

        public ChatServices()
        {
            _users = new List<User>();
            var _timer = new System.Timers.Timer(5000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Enabled = true;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = _users.Count - 1; i >= 0; i--)
            {
                if ((DateTime.Now - _users.ElementAt(i).LastPing).Seconds > 5)
                {
                    _users.RemoveAt(i);
                }
            }
        }

        public void SendMessage(Message message)
        {
            var user = _users.Where(u => u.Pseudo == message.Receiver).FirstOrDefault();
            if (user == null)
                return;
            user.Messages.Add(message);
            user.IsDirty = true;
        }

        public bool CreateUser(User user)
        {
            var newUser = new ChatDal().CreateUser(user);
            if (newUser == null)
            {
                return false;
            }
            _users.Add(newUser);
            return true;
        }

        public bool ConnectUser(string pseudo)
        {
            var newUser = new ChatDal().ConnectUser(pseudo);
            var userConnected = _users.Find(u => u.Pseudo == pseudo);
            if (newUser == null || userConnected != null)
            {
                return false;
            }
            
            _users.Add(newUser);
            return true;
        }

        public List<Message> GetNewMessages(string pseudo)
        {
            var user = _users.Where(u => u.Pseudo == pseudo).FirstOrDefault();
            user.LastPing = DateTime.Now;
            if (user.IsDirty)
            {
                user.IsDirty = false;
                var messages = new List<Message>(user.Messages);
                user.Messages.Clear();
                return messages;
            }

            return null;
        }

        public List<string> GetConnectedUsers()
        {
            var usersPseudo = new List<string>();
            foreach (var user in _users)
            {
                usersPseudo.Add(user.Pseudo);
            }
            return usersPseudo;
        }
    }
}
