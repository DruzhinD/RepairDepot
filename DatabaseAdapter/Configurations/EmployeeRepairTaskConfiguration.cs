using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class EmployeeRepairTaskConfiguration : IEntityTypeConfiguration<EmployeeRepairTask>
    {
        public void Configure(EntityTypeBuilder<EmployeeRepairTask> entity)
        {
            entity
                .HasNoKey()
                .ToTable("Employee_RepairTask");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Employee_Id");
            entity.Property(e => e.RepairTaskId)
                .ValueGeneratedOnAdd()
                .HasColumnName("RepairTask_Id");

            entity.HasOne(d => d.Employee).WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_RepairTask_Employee_Id_fkey");

            entity.HasOne(d => d.RepairTask).WithMany()
                .HasForeignKey(d => d.RepairTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_RepairTask_RepairTask_Id_fkey");
        }
    }
}
