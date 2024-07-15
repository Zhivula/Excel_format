using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZHIVULA.Data;

namespace ZHIVULA.DataBase
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        {

        }
        public DbSet<Cell_1> Cell_1 { get; set; }
        public DbSet<Cell_2> Cell_2 { get; set; }
    }
}