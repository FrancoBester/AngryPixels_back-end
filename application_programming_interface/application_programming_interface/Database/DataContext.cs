using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace application_programming_interface.Models
{
    public interface IDataContext
    {
        DbSet<Address> Address { get; set; }
        DbSet<Admissions> Admissions { get; set; }
        DbSet<Document> Document { get; set; }
        DbSet<Medical_Certificate> Medical_Certificates { get; set; }
        DbSet<Policy> Policy { get; set; }
        DbSet<Queries> Queries { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<User_Type> User_Type { get; set; }
        DbSet<Users> Users { get; set; }
    }

    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admissions>().HasOne(a => a.Policy).WithMany(p => p.Admissions).HasForeignKey(a => a.Policy_id); //tested policy/admission
            modelBuilder.Entity<User_Type>().HasOne(u => u.Roles).WithMany(r => r.User_Types).HasForeignKey(u => u.Role_id); //tested user_type/roles
            modelBuilder.Entity<Medical_Certificate>().HasOne(m => m.Document).WithMany(d => d.Medical_Certificates).HasForeignKey(m => m.Med_Cet_id);//not test medical/document
            modelBuilder.Entity<Users>().HasOne(u => u.Query).WithMany(q => q.Users).HasForeignKey(u => u.Query_id); // not test user/queries
            modelBuilder.Entity<Users>().HasOne(u => u.Address).WithMany(a => a.Users).HasForeignKey(u => u.Address_id);// not test user/address
            modelBuilder.Entity<Users>().HasOne(u => u.User_Type).WithMany(t => t.Users).HasForeignKey(u => u.User_type_id);// not test user/user_type
            modelBuilder.Entity<Users>().HasOne(u => u.Medical_Certificate).WithMany(m => m.Users).HasForeignKey(u => u.Medical_Certificate);// not test user/medical
            modelBuilder.Entity<Users>().HasOne(u => u.Policy).WithMany(p => p.Users).HasForeignKey(u => u.User_id);//not tested user/policy
            modelBuilder.Entity<Users>().HasOne(u => u.Document).WithMany(d => d.Users).HasForeignKey(u => u.Doc_id);//not tested user/document


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
