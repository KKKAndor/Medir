using Medir.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Persistence.EntityTypeConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(a => a.CityId);
            builder.HasIndex(a => a.CityId).IsUnique();

            builder
                .HasMany(c => c.Polyclinics)
                .WithOne(p => p.City)
                .HasForeignKey(p=>p.CityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
