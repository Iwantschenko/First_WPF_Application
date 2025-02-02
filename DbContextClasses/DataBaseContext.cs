﻿using Microsoft.EntityFrameworkCore;
namespace DbContextClasses
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<GroupStudent> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        
        public DataBaseContext()
        {
           
            Database.Migrate();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=DbWPF;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}