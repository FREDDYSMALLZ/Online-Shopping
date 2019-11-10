using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Online_Shopping.Models.AdminData
{
    public class MyDb : DbContext
    {
        public DbSet<PageDTO> Pages { get; set; }
    }
}