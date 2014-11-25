using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace ChatApi.Models
{
    public class User
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public bool IsDirty { get; set; }
        [PrimaryKey]
        public string Pseudo { get; set; }
        [Ignore]
        public List<Message> Messages { get; set; }
        [Ignore]
        public DateTime LastPing { get; set; }
        public User()
        {
            IsDirty = false;
            Messages = new List<Message>();
        }
    }
}