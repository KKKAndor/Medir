
namespace Medir.Application.User.Queries.GetPositionsForUserList
{
    public class PositionForUserLookUpDto
    {
        public Guid PositionId { get; set; }

        public string PositionName { get; set; } = string.Empty;

        public int MedicCount { get; set; } = 0;
    }
}
