using Medir.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Medir.Persistence.EntityTypeConfigurations
{
    public class MedicAppointmentAvailabilityConfiguration : IEntityTypeConfiguration<MedicAppointmentAvailability>
    {
        public void Configure(EntityTypeBuilder<MedicAppointmentAvailability> builder)
        {
            builder
                .HasKey(maa => maa.MedicAppointmentAvailabilityId);

            builder
                .HasOne(m => m.Appointment)
                .WithOne(m => m.MedicAppointmentAvailability)
                .HasForeignKey<Appointment>(m => m.MedicAppointmentAvailabilityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(m => m.Polyclinic)
                .WithMany(p => p.MedicAppointmentAvailabilities)
                .HasForeignKey(p => p.PolyclinicId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(m => m.Medic)
                .WithMany(p => p.MedicAppointmentAvailabilities)
                .HasForeignKey(p => p.MedicId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(m => m.Position)
                .WithMany(p => p.MedicAppointmentAvailabilities)
                .HasForeignKey(p => p.PositionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
