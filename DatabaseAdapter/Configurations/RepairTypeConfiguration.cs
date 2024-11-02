using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class RepairTypeConfiguration : IEntityTypeConfiguration<RepairType>
    {
        public void Configure(EntityTypeBuilder<RepairType> entity)
        {
            entity.HasKey(e => e.Id).HasName("RepairType_pkey");

            entity.ToTable("RepairType", tb => tb.HasComment("Справочник тип ремонта"));

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("type");
        }
    }
}
