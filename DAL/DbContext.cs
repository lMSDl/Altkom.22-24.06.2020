using DAL.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbContext : System.Data.Entity.DbContext
    {
        public DbContext() : base("Server=(local)\\SQLEXPRESS; Initial Catalog=School;Integrated Security=True")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbContext, DAL.Migrations.Configuration>(true));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
