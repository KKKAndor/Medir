using MediatR;
using Medir.Application.Common.Exceptions;
using Medir.Application.Interfaces;
using Medir.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Medics.Commands.UpdateMedic
{
    public class UpdateMedicCommandHandler
        : IRequestHandler<UpdateMedicCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public UpdateMedicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateMedicCommand request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Medics.FirstOrDefaultAsync(a =>
                a.MedicId == request.MedicId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Medic), request.MedicId);
            }

            entity.MedicFullName = request.MedicFullName;
            entity.FullDescription = request.FullDescription;
            entity.ShortDescription = request.ShortDescription;
            entity.MedicPhoneNumber = request.MedicPhoneNumber;
            entity.MedicPhoto = request.MedicPhoto;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
