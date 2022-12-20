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
    public class PolyclinicConfiguration : IEntityTypeConfiguration<Polyclinic>
    {
        public void Configure(EntityTypeBuilder<Polyclinic> builder)
        {
            builder.HasKey(a => a.PolyclinicId);
            builder.HasIndex(a => a.PolyclinicId).IsUnique();
        }
    }
}
