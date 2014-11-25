using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDP.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public CDPEnum.UserLevel Level {get;set;} 
        public DateTime BirthDate { get; set; }
    }
}
