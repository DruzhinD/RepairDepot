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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user", tb => tb.HasComment("пользователи приложения"));

            entity.HasIndex(e => e.Login, "login_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("last_name");
            entity.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.MiddleName)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.Users)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("user_permission_id_fkey");
        }
    }
}
