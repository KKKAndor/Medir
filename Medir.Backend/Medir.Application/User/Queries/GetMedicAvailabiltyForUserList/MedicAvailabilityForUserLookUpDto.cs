using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList;
using Medir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.User.Queries.GetMedicAvailabiltyForUserList
{
    public class MedicAvailabilityForUserLookUpDto : IMapWith<MedicAppointmentAvailability>
    {
        public Guid MedicAppointmentAvailabilityId { get; set; }

        public Guid MedicId { get; set; }

        public Guid PositionId { get; set; }

        public Guid PolyclinicId { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MedicAppointmentAvailability, MedicAvailabilityForUserLookUpDto>()
                .ForMember(Vm => Vm.MedicAppointmentAvailabilityId,
                    opt => opt.MapFrom(ap => ap.MedicAppointmentAvailabilityId))
                .ForMember(Vm => Vm.TimeStart,
                    opt => opt.MapFrom(ap => ap.TimeStart))
                .ForMember(Vm => Vm.TimeEnd,
                    opt => opt.MapFrom(ap => ap.TimeEnd))
                .ForMember(Vm => Vm.Date,
                    opt => opt.MapFrom(ap => ap.Date))
                .ForMember(Vm => Vm.MedicId,
                    opt => opt.MapFrom(ap => ap.MedicId))
                .ForMember(Vm => Vm.PositionId,
                    opt => opt.MapFrom(ap => ap.PositionId))
                .ForMember(Vm => Vm.PolyclinicId,
                    opt => opt.MapFrom(ap => ap.PolyclinicId));
        }
    }
}
