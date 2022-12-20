using Medir.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Interfaces
{
    public interface IMedirDbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Polyclinic> Polyclinics { get; set; }
        public DbSet<MedicPolyclinic> MedicPolyclinics { get; set; }
        public DbSet<MedicPosition> MedicPositions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicAppointmentAvailability> MedicAppointmentAvailabilities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
