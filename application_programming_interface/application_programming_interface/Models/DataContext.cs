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
        public DbSet<Admissions> Admissions { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Medical_Certificate> Medical_Certificates { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<Queries> Queries { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User_Type> User_Type { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
