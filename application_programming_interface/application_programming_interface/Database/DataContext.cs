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
        DbSet<Document_Type> Document_Type { get; set; }
        DbSet<Policy> Policy { get; set; }
        DbSet<Queries> Queries { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<Schema_Requests> Schema_Requests { get; set; }
        DbSet<User_Policy> User_Policy { get; set; }
        DbSet<User_Roles> User_Roles { get; set; }
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
            //not tested
            modelBuilder.Entity<Users>().HasOne(u => u.Address).WithMany(a => a.Users).HasForeignKey(u => u.Address_Id);// user/address
            modelBuilder.Entity<Queries>().HasOne(q => q.Users).WithMany(u => u.Queries).HasForeignKey(q => q.User_Id); //query/users
            modelBuilder.Entity<User_Roles>().HasOne(ur => ur.Role).WithMany(r => r.User_Roles).HasForeignKey(ur => ur.Role_Id); // user_roles/roles
            modelBuilder.Entity<User_Roles>().HasOne(ur => ur.User).WithMany(u => u.User_Roles).HasForeignKey(ur => ur.User_Id);// user_roles/user
            modelBuilder.Entity<Schema_Requests>().HasOne(sr => sr.User).WithMany(u => u.Schema_Requests).HasForeignKey(sr => sr.User_Id);// schema_roles/user
            modelBuilder.Entity<User_Policy>().HasOne(up => up.User).WithMany(u => u.User_Policy).HasForeignKey(up => up.User_Policy_Id);// user_policy/user
            modelBuilder.Entity<Document>().HasOne(d => d.User).WithMany(u => u.Documents).HasForeignKey(d => d.User_Id);// document/user
            modelBuilder.Entity<Schema_Requests>().HasOne(sr => sr.Policy).WithMany(p => p.Schema_Requests).HasForeignKey(sr => sr.Policy_Id);// schema_request/policy
            modelBuilder.Entity<User_Policy>().HasOne(up => up.Policy).WithMany(p => p.User_Policies).HasForeignKey(up => up.Policy_Id);// user_policy/policy
            modelBuilder.Entity<Document>().HasOne(d => d.Policy).WithMany(p => p.Documents).HasForeignKey(d => d.Policy_Id);// document/policy
            modelBuilder.Entity<Admissions>().HasOne(a => a.Policy).WithMany(p => p.Admissions).HasForeignKey(a => a.Policy_Id);// admissions/policy
            modelBuilder.Entity<Document_Type>().HasOne(dt => dt.Document).WithMany(d => d.Document_Types).HasForeignKey(dt => dt.Doc_Id);// document_type/document
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Admissions> Admissions { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Document_Type> Document_Type { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<Queries> Queries { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Schema_Requests> Schema_Requests { get; set; }
        public DbSet<User_Policy> User_Policy { get; set; }
        public DbSet<User_Roles> User_Roles { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
