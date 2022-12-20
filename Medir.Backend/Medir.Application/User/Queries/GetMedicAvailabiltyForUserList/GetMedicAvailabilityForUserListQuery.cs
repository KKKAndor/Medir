using MediatR;

namespace Medir.Application.User.Queries.GetMedicAvailabiltyForUserList
{
    public class GetMedicAvailabilityForUserListQuery : IRequest<MedicAvailabilityForUserListVm>
    {
        public Guid MedicId { get; set; }

        public Guid PositionId { get; set; }

        public Guid PolyclinicId { get; set; }

        public DateTime Date { get; set; }
    }
}
