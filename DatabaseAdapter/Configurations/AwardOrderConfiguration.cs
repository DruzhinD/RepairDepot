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
    public class AwardOrderConfiguration : IEntityTypeConfiguration<AwardOrder>
    {
        public void Configure(EntityTypeBuilder<AwardOrder> builder)
        {
            builder.HasKey(e => e.Id).HasName("AwardOrder_pkey");

            builder.ToTable("AwardOrder", tb => tb.HasComment("Приказ о начислении премии"));

            builder.HasIndex(e => e.QualityControlId, "AwardOrder_quality_control_id_key").IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Bonus)
                .HasColumnType("money")
                .HasColumnName("bonus");
            builder.Property(e => e.BonusPercent).HasColumnName("bonus_percent");
            builder.Property(e => e.QualityControlId).HasColumnName("quality_control_id");

            builder.HasOne(d => d.QualityControl).WithOne(p => p.AwardOrder)
                .HasForeignKey<AwardOrder>(d => d.QualityControlId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_award_order_quality_control");
        }
    }
}
