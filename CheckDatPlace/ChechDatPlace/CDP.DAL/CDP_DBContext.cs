using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDP.Models;

namespace CDP.DAL
{
    public class CDP_DBContext: DbContext
    {

        private static CDP_DBContext _instance;
        public static CDP_DBContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CDP_DBContext();
                }
                return _instance;
            }
        }
        public CDP_DBContext(): base("CDP_DBContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
