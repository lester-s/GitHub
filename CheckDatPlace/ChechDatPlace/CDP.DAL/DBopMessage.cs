using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CDP.Models;

namespace CDP.DAL
{
    public class DBopMessage<T>: DBopMessage
    {
        public T Value { get; set; }
        
        public DBopMessage(HttpStatusCode status, string message, T value = default(T))
        {
            Status = status;
            Message = message;
            Value = value;
        }
       
    }

    public class DBopMessage
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }

        public DBopMessage(HttpStatusCode status, string message)
        {
            Status = status;
            Message = message;
        }

        public DBopMessage()
        {

        }
    }
}
