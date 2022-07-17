using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using GamMostre.Models;

namespace GamMostre.Migrations
{    
    internal sealed class Configuration : DbMigrationsConfiguration<MostreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MostreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
