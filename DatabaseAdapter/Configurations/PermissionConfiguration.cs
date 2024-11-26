using DatabaseAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAdapter.Configurations
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.HasKey(e => e.Id).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Admin)
                .HasDefaultValue(false)
                .HasColumnName("admin");
            entity.Property(e => e.PlaningDepartment)
                .HasDefaultValue(false)
                .HasColumnName("planing_department");
            entity.Property(e => e.RepairDepartment)
                .HasDefaultValue(false)
                .HasColumnName("repair_department");
            entity.Property(e => e.StaffDepartment)
                .HasDefaultValue(false)
                .HasColumnName("staff_department");
            entity.Property(e => e.TechnicalDepartment)
                .HasDefaultValue(false)
                .HasColumnName("technical_department");
        }
    }
}
