using Medir.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Medir.Persistence.EntityTypeConfigurations
{
    public class MedicPositionConfiguration : IEntityTypeConfiguration<MedicPosition>
    {
        public void Configure(EntityTypeBuilder<MedicPosition> builder)
        {
            builder
                .HasKey(mpp => new { mpp.MedicId, mpp.PositionId });

            builder
                .HasOne(mpp => mpp.Medic)
                .WithMany(m => m.MedicPositions)
                .HasForeignKey(mpp => mpp.MedicId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(mpp => mpp.Position)
                .WithMany(m => m.MedicPositions)
                .HasForeignKey(mpp => mpp.PositionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
