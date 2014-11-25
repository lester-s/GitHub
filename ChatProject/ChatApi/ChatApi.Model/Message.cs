using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace ChatApi.Models
{
    public class Message
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }

        public string Receiver { get; set; }
        public string Emitter { get; set; }
        public string Content { get; set; }
    }
}