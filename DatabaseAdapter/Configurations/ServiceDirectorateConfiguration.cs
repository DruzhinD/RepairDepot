using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class ServiceDirectorateConfiguration : IEntityTypeConfiguration<ServiceDirectorate>
    {
        public void Configure(EntityTypeBuilder<ServiceDirectorate> entity)
        {
            entity.HasKey(e => e.Id).HasName("service_directorate_pkey");

            entity.ToTable("service_directorate", tb => tb.HasComment("Дирекция по обслуживанию пассажиров"));

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Directorate)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("directorate");
        }
    }
}
