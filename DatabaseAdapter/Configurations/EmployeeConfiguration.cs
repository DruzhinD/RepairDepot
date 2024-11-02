using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseAdapter.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.HasKey(e => e.Id).HasName("Employee_pkey");

            entity.ToTable("Employee", tb => tb.HasComment("Сотрудник"));

            entity.Property(e => e.BankCard)
                .IsRequired()
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("bank_card");
            entity.Property(e => e.Education)
                .HasMaxLength(60)
                .HasColumnName("education");
            entity.Property(e => e.Experience).HasColumnName("experience");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.Specialization)
                .HasMaxLength(30)
                .HasColumnName("specialization");
        }
    }
}
