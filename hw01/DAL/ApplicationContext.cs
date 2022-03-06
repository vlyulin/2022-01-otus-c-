using Microsoft.EntityFrameworkCore;
using hw01.Model;

namespace hw01.DAL
{
    internal class ApplicationContext : DbContext
    {
        public ApplicationContext() : base()
        {
            Database.EnsureCreated();
            DBInitialize.Initialize(this);
        }

        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Server=ec2-52-214-125-106.eu-west-1.compute.amazonaws.com;Port=5432;Database=dfaesrvno37sqn;User Id=hwbberjdaazccj;Password=29e0407240dc76ac4f95743dac96a7a77e05de7f51ff48d4c1e48e47f62521d4;Pooling=true;Integrated Security=true;")
                .EnableSensitiveDataLogging();
        }
    }
}
