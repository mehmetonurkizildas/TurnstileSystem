using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence.EntityTypeConfigurations
{
    public class MovementReportConfiguration : IEntityTypeConfiguration<MovementReport>
    {
        public void Configure(EntityTypeBuilder<MovementReport> builder)
        {
        }
    }
}
