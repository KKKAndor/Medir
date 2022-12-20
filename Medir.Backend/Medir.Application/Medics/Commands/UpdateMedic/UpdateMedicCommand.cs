using MediatR;

namespace Medir.Application.Medics.Commands.UpdateMedic
{
    public class UpdateMedicCommand : IRequest
    {
        public Guid MedicId { get; set; }

        public string MedicFullName { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string FullDescription { get; set; } = string.Empty;

        public string MedicPhoneNumber { get; set; } = string.Empty;

        public string MedicPhoto { get; set; } = string.Empty;
    }
}
