using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<MovementReport> MovementReports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var faker = new Bogus.Faker("tr");
            List<Person> persons = new();
            for (int i = 1; i <= 25; i++)
            {
                persons.Add(new Person
                {
                    Id = i,
                    Email = faker.Internet.Email().ToLower(),
                    FirstName = faker.Name.FirstName(),
                    LastName = faker.Name.LastName(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                });
            }

            List<Movement> movements = new();
            for (int i = 1; i <= 25; i++)
            {
                movements.Add(new Movement
                {
                    Id = i,
                    PersonId = i,
                    MovementTime = faker.Date.Between(DateTime.UtcNow.AddHours(-5), DateTime.UtcNow.AddHours(5)),
                    EventType = EventType.Login,
                });
            }

            modelBuilder.Entity<Person>().HasData(persons);
            modelBuilder.Entity<Movement>().HasData(movements);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
