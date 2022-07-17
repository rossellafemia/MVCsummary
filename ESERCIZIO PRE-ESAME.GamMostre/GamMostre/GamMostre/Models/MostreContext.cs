using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GamMostre.Models
{
    public class MostreContext:DbContext
    {
        public DbSet<Autore> Autori { get; set; }
        public DbSet<Mostra> Mostre { get; set; }
        
        public MostreContext()
        {
            Database.Connection.ConnectionString = 
                ConfigurationManager.ConnectionStrings["MostreConnection"].ConnectionString;
        }
    }
}