using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace application_programming_interface.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { 
        }

        public DbSet<Address> Address { get; set; }

        public DbSet<Document> Document { get; set; }
    }
}
