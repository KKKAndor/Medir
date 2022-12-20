namespace Medir.Application.User.Queries.GetMedicsForUserList
{
    public class MedicForUserLookUpDto
    {
        public Guid MedicId { get; set; }

        public Guid PositionId { get; set; }

        public string MedicFullName { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string FullDescription { get; set; } = string.Empty;

        public string MedicPhoneNumber { get; set; } = string.Empty;

        public string MedicPhoto { get; set; } = string.Empty;

        public int YearsOnPosition { get; set; }

        public IList<string> PositionNames { get; set; }

        public IList<MedicPolyclinicForUserLookUpDto> MedicPolyclinicForUserLookUpList { get; set; }
    }
}
