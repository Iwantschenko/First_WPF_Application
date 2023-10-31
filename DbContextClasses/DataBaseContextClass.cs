using Microsoft.EntityFrameworkCore;
namespace DbContextClasses
{
    public class DataBaseContextClass : DbContext
    {
        DbSet<Course> Courses { get; set; }
        DbSet<GroupStudent> Groups { get; set; }
        DbSet<Student> Students { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        public DataBaseContextClass() 
        {
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=DbWPF;Trusted_Connection=True;");
        }
    }
}