using Medir.Application.Interfaces;
using Medir.Domain;
using Medir.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Medir.Persistence
{
    public class MedirDbContext : DbContext, IMedirDbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Medic> Medics { get; set; } = null!;
        public DbSet<Position> Positions { get; set; } = null!; 
        public DbSet<Polyclinic> Polyclinics { get; set; } = null!;
        public DbSet<MedicPolyclinic> MedicPolyclinics { get; set; } = null!;
        public DbSet<MedicPosition> MedicPositions { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<MedicAppointmentAvailability> MedicAppointmentAvailabilities { get; set; } = null!;

        public MedirDbContext(DbContextOptions<MedirDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
                       

            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new MedicConfiguration());            
            builder.ApplyConfiguration(new PolyclinicConfiguration());
            builder.ApplyConfiguration(new PositionConfiguration());
            builder.ApplyConfiguration(new MedicPositionConfiguration());
            builder.ApplyConfiguration(new MedicPolyclinicConfiguration());
            builder.ApplyConfiguration(new MedicAppointmentAvailabilityConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());
        }

        public static string ConnectionString
        {
            get
            {
                return "PostgreSQLConnection";
                //switch (Environment.MachineName)
                //{
                //    case "DESKTOP-4EJEMKI":
                //        return "SqlNoteConnection";
                //    case "DESKTOP-9ACRVR9":
                //        return "SqlPCConnection";
                //    default:
                //        return "SqlLocalConnection";
                //}
            }                  
        }

        
    }
}
