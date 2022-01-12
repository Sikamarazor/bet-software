using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BETSoftware.Entities;
using Microsoft.EntityFrameworkCore;

namespace BETSoftware.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}