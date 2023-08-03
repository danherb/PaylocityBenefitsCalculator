using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Db
{
    public class CustomDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        public CustomDbContext(DbContextOptions<CustomDbContext> options)
        : base(options)
        {
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().Ignore(e => e.Age);

            // Seed some employee and dependents
    //        modelBuilder.Entity<Employee>().HasData(
    //            new Employee
    //            {
    //                Id = 1000,
    //                DateOfBirth = new DateTime(1951, 1, 1),
    //                FirstName = "Natalie",
    //                LastName = "Novak",
    //                Salary = 52000
    //            }
    //        );

    //        modelBuilder.Entity<Dependent>().HasData(
    //            new Dependent
    //            {
    //                Id = 1001,
    //                DateOfBirth = new DateTime(1950, 1, 1),
    //                FirstName = "Adam",
    //                LastName = "Novak",
    //                Relationship = Relationship.Spouse,
    //                EmployeeId = 1000
				//},
    //               new Dependent
    //               {
    //                   Id = 1002,
    //                   DateOfBirth = new DateTime(2000, 1, 1),
    //                   FirstName = "Peter",
    //                   LastName = "Novak",
    //                   Relationship = Relationship.Child,
    //                   EmployeeId = 1000
				//   },
    //                  new Dependent
    //                  {
    //                      Id = 1003,
    //                      DateOfBirth = new DateTime(2001, 1, 1),
    //                      FirstName = "Ashley",
    //                      LastName = "Novak",
    //                      Relationship = Relationship.Child,
    //                      EmployeeId = 1000
				//	  }
    //        );
        }

    }
}
