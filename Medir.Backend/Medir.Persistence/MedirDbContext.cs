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

            FirstInitialize(builder);
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

        private void FirstInitialize(ModelBuilder builder)
        {
            City Tiraspol = new City
            {
                CityId = new Guid("2d10a186-2066-41d6-81c5-e2989584b1f7"),
                CityName = "Тирасполь",
                Latitude = 29.611113,
                Longitude = 46.835991
            };
            City Bender = new City
            {
                CityId = new Guid("4763640b-ab8b-49c9-b9db-fb29ce90a32d"),
                CityName = "Бендеры",
                Latitude = 29.481899,
                Longitude = 46.823089
            };

            builder.Entity<City>().HasData(
                new City[]
                {
                    Tiraspol,
                    Bender
                }
            );

            Position Therapist = new Position
            {
                PositionId = new Guid("e702bc99-16de-44a1-bf05-44bf42433e11"),
                PositionName = "Терапевт"
            };
            Position UltrasoundDoctor = new Position
            {
                PositionId = new Guid("37aa319f-9248-48e3-886e-12ad6bcbde51"),
                PositionName = "Врач ультразвуковой диагностики"
            };
            Position Endocrinologist = new Position
            {
                PositionId = new Guid("5d89f79d-4c8c-4df7-81e1-ccf633e5f3ea"),
                PositionName = "Эндокринолог"
            };
            Position Cardiologist = new Position
            {
                PositionId = new Guid("115d8667-1de7-44c9-a15f-f0be3ccb901a"),
                PositionName = "Кардиолог"
            };
            Position FunctionalDiagnosticsDoctor = new Position
            {
                PositionId = new Guid("92ac6b2a-7d9d-4d69-8fbe-48a8e7e77fc8"),
                PositionName = "Врач функциональной диагностики"
            };

            builder.Entity<Position>().HasData(
                new Position[]
                {
                    Therapist,
                    UltrasoundDoctor,
                    Endocrinologist,
                    Cardiologist,
                    FunctionalDiagnosticsDoctor
                }
            );

            Polyclinic Medin = new Polyclinic
            {
                PolyclinicId = new Guid("7aa4f4cb-33de-4ae9-b195-b3b790b419f1"),
                CityId = Tiraspol.CityId,
                Latitude = 29.559985,
                Longitude = 46.841977,
                PolyclinicAddress = "ул. Карла Либкнехта, 1/2",
                PolyclinicPhoneNumber = "+37353361818",
                PolyclinicName = "Медин",
                PolyclinicPhoto = "Resources\\Images\\Медин.png"
            };
            Polyclinic CentralBenderPolyclinic = new Polyclinic
            {
                PolyclinicId = new Guid("02b2f2b0-f7ce-4a7b-af2c-847b057f8feb"),
                CityId = Bender.CityId,
                Latitude = 29.486502,
                Longitude = 46.822804,
                PolyclinicAddress = "ул. Сергея Лазо, 20",
                PolyclinicPhoneNumber = "+37355221366",
                PolyclinicName = "Центральная бендерская поликлиника",
                PolyclinicPhoto = "Resources\\Images\\ЦентральнаяПоликлиникаБендер.png"
            };
            Polyclinic Hydropathic = new Polyclinic
            {
                PolyclinicId = new Guid("18f20631-76da-4797-8122-a8530f701154"),
                CityId = Tiraspol.CityId,
                Latitude = 29.597313,
                Longitude = 46.838322,
                PolyclinicAddress = "ул. Карла Маркса, 1",
                PolyclinicPhoneNumber = "+37353393415",
                PolyclinicName = "Водолечебница",
                PolyclinicPhoto = "Resources\\Images\\Водолечебница.jpg"
            };

            builder.Entity<Polyclinic>().HasData(
                new Polyclinic[]
                {
                    Medin,
                    CentralBenderPolyclinic,
                    Hydropathic
                }
            );

            Medic Litvin = new Medic
            {
                MedicId = new Guid("05a3eb81-ec1f-46b7-8593-ee45c1aee8f8"),
                MedicFullName = "Литвин Вероника Юрьевна",
                ShortDescription = "Врач первой категории",
                FullDescription = "Выглядит опасно",
                MedicPhoneNumber = "+37353360001",
                MedicPhoto = "Resources\\Images\\ЛитвинВероникаЮрьевна.jpg"
            };

            Medic Likrizon = new Medic
            {
                MedicId = new Guid("8a285c83-9ce6-4213-b0e9-f1d8ce4a7c84"),
                MedicFullName = "Ликризон Сергей Вячеславович",
                ShortDescription = "Врач второй категории",
                FullDescription = "Обычный врач",
                MedicPhoneNumber = "+37353360002",
                MedicPhoto = "Resources\\Images\\ЛикризонСергейВячеславович.jpg"
            };
            Medic Vasilieva = new Medic
            {
                MedicId = new Guid("86f765c2-d763-4f5b-beea-c0d2627932ab"),
                MedicFullName = "Васильева Вера Михайловна",
                ShortDescription = "Врач первой категории",
                FullDescription = "Какое-то описание врача очень длинное",
                MedicPhoneNumber = "+37377718821",
                MedicPhoto = "Resources\\Images\\ВасильеваВераМихайловна.jpg"
            };
            Medic Polerka = new Medic
            {
                MedicId = new Guid("e0d2c957-983c-4678-9a05-a24fad0e3330"),
                MedicFullName = "Полерка Екатерина Алексеевна",
                ShortDescription = "Врач второй категории",
                FullDescription = "Дешево но сердито",
                MedicPhoneNumber = "+37353393001",
                MedicPhoto = "Resources\\Images\\ПолеркаЕкатеринаАлексеевна.jpg"
            };
            Medic Stecuk = new Medic
            {
                MedicId = new Guid("0ad883bc-5fe5-4396-9c8b-12905df0815d"),
                MedicFullName = "Стецюк Елена Владимировна",
                ShortDescription = "Врач",
                FullDescription = "Таки кардиолог",
                MedicPhoneNumber = "+37353360005",
                MedicPhoto = "Resources\\Images\\СтецюкЕленаВладимировна.jpg"
            };
            Medic Trepak = new Medic
            {
                MedicId = new Guid("ef1bff42-6af5-459b-bc16-fe45838955ec"),
                MedicFullName = "Трепак Андрей Ефимович",
                ShortDescription = "Врач",
                FullDescription = "Слишком добрый чтобы быть врачом",
                MedicPhoneNumber = "+37353360006",
                MedicPhoto = "Resources\\Images\\ТрепакАндрейЕфимович.jpg"
            };

            builder.Entity<Medic>().HasData(
                new Medic[]
                {
                    Trepak,
                    Stecuk,
                    Polerka,
                    Vasilieva,
                    Likrizon,
                    Litvin
                });

            MedicPosition litpos1 = new MedicPosition
            {
                MedicId = Litvin.MedicId,
                PositionId = Therapist.PositionId,
                DateOnPosition = new DateTime(2005, 11, 12)
            };
            MedicPosition litpos2 = new MedicPosition
            {
                MedicId = Litvin.MedicId,
                PositionId = UltrasoundDoctor.PositionId,
                DateOnPosition = new DateTime(2015, 11, 12)
            };

            MedicPosition likpos1 = new MedicPosition
            {
                MedicId = Likrizon.MedicId,
                PositionId = UltrasoundDoctor.PositionId,
                DateOnPosition = new DateTime(2007, 11, 12)
            };
            MedicPosition likpos2 = new MedicPosition
            {
                MedicId = Likrizon.MedicId,
                PositionId = Endocrinologist.PositionId,
                DateOnPosition = new DateTime(2016, 11, 12)
            };

            MedicPosition vaspos = new MedicPosition
            {
                MedicId = Vasilieva.MedicId,
                PositionId = Therapist.PositionId,
                DateOnPosition = new DateTime(2000, 11, 12)
            };

            MedicPosition polpos = new MedicPosition
            {
                MedicId = Polerka.MedicId,
                PositionId = Therapist.PositionId,
                DateOnPosition = new DateTime(2017, 11, 12)
            };

            MedicPosition stepos = new MedicPosition
            {
                MedicId = Stecuk.MedicId,
                PositionId = Cardiologist.PositionId,
                DateOnPosition = new DateTime(2016, 11, 12)
            };

            MedicPosition trepos = new MedicPosition
            {
                MedicId = Trepak.MedicId,
                PositionId = FunctionalDiagnosticsDoctor.PositionId,
                DateOnPosition = new DateTime(2020, 11, 12)
            };

            builder.Entity<MedicPosition>().HasData(
                new MedicPosition[]
                {
                    litpos1,
                    litpos2,
                    likpos1,
                    likpos2,
                    vaspos,
                    polpos,
                    stepos,
                    trepos
                });

            MedicPolyclinic litpol = new MedicPolyclinic
            {
                MedicId = Litvin.MedicId,
                PolyclinicId = Medin.PolyclinicId,
                Price = 325
            };

            MedicPolyclinic likpol = new MedicPolyclinic
            {
                MedicId = Likrizon.MedicId,
                PolyclinicId = Medin.PolyclinicId,
                Price = 325
            };

            MedicPolyclinic vaspol1 = new MedicPolyclinic
            {
                MedicId = Vasilieva.MedicId,
                PolyclinicId = CentralBenderPolyclinic.PolyclinicId,
                Price = 100
            };
            MedicPolyclinic vaspol2 = new MedicPolyclinic
            {
                MedicId = Vasilieva.MedicId,
                PolyclinicId = Hydropathic.PolyclinicId,
                Price = 125
            };

            MedicPolyclinic polpol1 = new MedicPolyclinic
            {
                MedicId = Polerka.MedicId,
                PolyclinicId = Hydropathic.PolyclinicId,
                Price = 125
            };

            MedicPolyclinic stepol1 = new MedicPolyclinic
            {
                MedicId = Stecuk.MedicId,
                PolyclinicId = CentralBenderPolyclinic.PolyclinicId,
                Price = 100
            };

            MedicPolyclinic trepol1 = new MedicPolyclinic
            {
                MedicId = Trepak.MedicId,
                PolyclinicId = Medin.PolyclinicId,
                Price = 250
            };

            builder.Entity<MedicPolyclinic>().HasData(
                new MedicPolyclinic[]
                {
                    litpol,
                    likpol,
                    vaspol1,
                    vaspol2,
                    polpol1,
                    stepol1,
                    trepol1
                });
        }
    }
}
