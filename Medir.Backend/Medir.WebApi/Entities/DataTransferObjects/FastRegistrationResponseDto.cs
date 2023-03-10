namespace Medir.WebApi.Entities.DataTransferObjects
{
    public class FastRegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }

        public IEnumerable<string>? Errors { get; set; }
    }
}
