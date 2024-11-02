using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
    {
        public void Configure(EntityTypeBuilder<Worker> entity)
        {
            entity.HasKey(e => e.Id).HasName("Worker_pkey");

            entity.ToTable("Worker", tb => tb.HasComment("Работник (рабочий)"));

            entity.HasIndex(e => e.EmployeeId, "Worker_employee_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChiefId).HasColumnName("chief_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

            entity.HasOne(d => d.Chief).WithMany(p => p.WorkerChiefs)
                .HasForeignKey(d => d.ChiefId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_worker_foreman");

            entity.HasOne(d => d.Employee).WithOne(p => p.WorkerEmployee)
                .HasForeignKey<Worker>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_worker_employee");
        }
    }
}
