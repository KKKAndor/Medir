using Medir.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Medir.Persistence.EntityTypeConfigurations
{
    public class MedicPolyclinicConfiguration : IEntityTypeConfiguration<MedicPolyclinic>
    {
        public void Configure(EntityTypeBuilder<MedicPolyclinic> builder)
        {
            builder
                .HasKey(mpp => new { mpp.MedicId, mpp.PolyclinicId });

            builder
                .HasOne(mpp => mpp.Medic)
                .WithMany(m => m.MedicPolyclinics)
                .HasForeignKey(mpp => mpp.MedicId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(mpp => mpp.Polyclinic)
                .WithMany(m => m.MedicPolyclinics)
                .HasForeignKey(mpp => mpp.PolyclinicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
