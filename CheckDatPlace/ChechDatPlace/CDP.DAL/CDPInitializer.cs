using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CDP.Models;

namespace CDP.DAL
{
    public class CDPInitializer: DropCreateDatabaseAlways<CDP_DBContext>
    {
        protected override void Seed(CDP_DBContext context)
        {
            var users = new List<User>
            {
                new User(){FirstName= "Simon", LastName = "LE STER"}
            };
            context.SaveChanges();
        }
    }
}
