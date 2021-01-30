using StoreLib.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreLib.Concrete
{
    class EFDbContext: DbContext
    {
        public EFDbContext() : base("name=Store")
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
    }
}
