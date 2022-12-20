using Medir.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Medir.Persistence.EntityTypeConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .HasKey(a => a.AppointmentId);
            builder.HasIndex(a => a.AppointmentId).IsUnique();
        }
    }
}
