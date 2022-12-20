using FluentValidation;

namespace Medir.Application.User.Queries.GetMedicsForUserList
{
    public class GetMedicsForUserListQueryValidator : AbstractValidator<GetMedicsForUserListQuery>
    {
        public GetMedicsForUserListQueryValidator()
        {
            RuleFor(command =>
               command.PositionId).NotEqual(Guid.Empty);
        }
    }
}
