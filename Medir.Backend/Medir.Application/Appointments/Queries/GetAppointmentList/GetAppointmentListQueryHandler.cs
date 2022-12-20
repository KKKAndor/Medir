using AutoMapper;
using MediatR;
using Medir.Application.Interfaces;
using Medir.Application.User.Queries.GetMedicsForUserList;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Appointments.Queries.GetAppointmentList
{
    public class GetAppointmentListQueryHandler
        : IRequestHandler<GetAppointmentListQuery, AppointmentsListVm>
    {
        private readonly IMedirDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetAppointmentListQueryHandler(IMedirDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<AppointmentsListVm> Handle(GetAppointmentListQuery request,
            CancellationToken cancellationToken)
        {
            var appointments = await
                _dbContext.Appointments
                .Where(a => a.UserEmail == request.UserEmail)
                .Join(
                    _dbContext.MedicAppointmentAvailabilities
                    .Include(m => m.Polyclinic).ThenInclude(p => p.City)
                    .Include(m => m.Medic)
                    .Include(m => m.Position),
                    u => u.AppointmentId,
                    c => c.AppointmentId,
                    (u, c) => new AppointmentLookUpDto
                    {
                        AppointmentId = u.AppointmentId,
                        CityName = c.Polyclinic.City.CityName,
                        MedicFullName = c.Medic.MedicFullName,
                        PolyclinicAddress = c.Polyclinic.PolyclinicAddress,
                        PolyclinicName = c.Polyclinic.PolyclinicName,
                        PositionName = c.Position.PositionName,
                        Prescription = u.Prescription,
                        Price = u.Price,
                        Time = c.TimeStart,
                        Date = c.Date
                    })
                .OrderBy(m => m.Date)
                .Reverse()
                .ToListAsync(cancellationToken);

            return new AppointmentsListVm { Appointments = appointments };
        }
    }
}
