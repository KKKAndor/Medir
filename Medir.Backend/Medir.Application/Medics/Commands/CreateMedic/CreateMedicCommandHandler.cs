using MediatR;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.Medics.Commands.CreateMedic
{
    public class CreateMedicCommandHandler
        : IRequestHandler<CreateMedicCommand, Guid>
    {
        private readonly IMedirDbContext _dbContext;

        public CreateMedicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(CreateMedicCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Medic
            {
                MedicId = Guid.NewGuid(),
                MedicFullName = request.MedicFullName,
                ShortDescription = request.ShortDescription,
                FullDescription = request.FullDescription,
                MedicPhoneNumber = request.MedicPhoneNumber,
                MedicPhoto = request.MedicPhoto
            };

            await _dbContext.Medics.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.MedicId;
        }
    }
}
