using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class RailwayConfiguration : IEntityTypeConfiguration<Railway>
    {
        public void Configure(EntityTypeBuilder<Railway> entity)
        {
            entity.HasKey(e => e.Id).HasName("Railway_pkey");

            entity.ToTable("Railway");

            entity.Property(e => e.External).HasDefaultValue(false);
        }
    }
}
