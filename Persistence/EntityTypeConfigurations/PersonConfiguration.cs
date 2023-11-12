using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfigurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Movements)
                .WithOne(x => x.Person)
                .HasForeignKey(x => x.PersonId);

            builder.HasMany(x => x.MovementReports)
              .WithOne(x => x.Person)
              .HasForeignKey(x => x.PersonId);

        }
    }
}
