using MediatR;
using Medir.Application.Interfaces;
using Medir.Domain;

namespace Medir.Application.MedicPolyclinics.Commands.CreateMedicPolyclinic
{
    public class CreateMedicPolyclinicCommandHandler
        : IRequestHandler<CreateMedicPolyclinicCommand>
    {
        private readonly IMedirDbContext _dbContext;

        public CreateMedicPolyclinicCommandHandler(IMedirDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(CreateMedicPolyclinicCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new MedicPolyclinic
            {
                MedicId = request.MedicId,
                Price = request.Price,
                PolyclinicId = request.PolyclinicId
            };

            await _dbContext.MedicPolyclinics.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
