using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeDotnet
{
    internal class ETradeContext:DbContext
    {
        public DbSet<Products> Products { get; set; }
    }
}
