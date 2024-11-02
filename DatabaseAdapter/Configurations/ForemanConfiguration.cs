using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class ForemanConfiguration : IEntityTypeConfiguration<Foreman>
    {
        public void Configure(EntityTypeBuilder<Foreman> entity)
        {
            entity.HasKey(e => e.Id).HasName("Foreman_pkey");

            entity.ToTable("Foreman", tb => tb.HasComment("Бригадир"));

            entity.HasIndex(e => e.EmployeeId, "Foreman_employee_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Employee).WithOne(p => p.Foreman)
                .HasForeignKey<Foreman>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_foreman_employee");
        }
    }
}
