using FluentValidation;
using CardService.Application.Queries;

namespace CardService.Application.Validators
{
    public class GetCardDetailsQueryValidator : AbstractValidator<GetCardDetailsQuery>
    {
        public GetCardDetailsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage("CardNumber is required.");
        }
    }
}