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
            modelBuilder.Entity<Admissions>().HasOne(a => a.Policy).WithMany(p => p.Admissions).HasForeignKey(a =>a.Policy_id);
            //modelBuilder.Entity<Admissions>().has
            //modelBuilder.Entity<Policy>().HasMany(p => p.Admissions).WithOne(a => a.Policy).HasForeignKey(a => a.Policy.Policy_id).IsRequired();
            //modelBuilder.Entity<Policy>().HasMany(p => p.Admissions);
            //modelBuilder.Entity<Admissions>().HasOne(a => a.Policy).WithMany(p => p.Admissions);
            //modelBuilder.Entity<Admissions>().HasOne(a => a.Policy).WithMany(p => p.Admissions).HasForeignKey(a => a.Policy.Policy_id);
            //modelBuilder.Entity<Policy>().HasMany(c => c.Admissions).WithOne(x => x.Policy).HasForeignKey(x => x.Policy_id);
            //modelBuilder.Entity<Policy>().HasMany(c => c.Admissions).WithOne(x => x.Policy).HasPrincipalKey(x => x.Policy_id);
            modelBuilder.Entity<Roles>().HasMany(c => c.User_Types).WithOne(x => x.Roles).HasPrincipalKey(x => x.Role_id);
            modelBuilder.Entity<Policy>().HasMany(c => c.Users).WithOne(x => x.Policy).HasForeignKey(c => c.User_id);

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
