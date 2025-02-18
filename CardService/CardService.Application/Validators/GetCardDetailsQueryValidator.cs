using CardService.Application.Interfaces;
using CardService.Application.Queries;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace CardService.Application.Validators
{
    public class GetCardDetailsQueryValidator : AbstractValidator<GetCardDetailsQuery>
    {
        private readonly ICardService _cardService;
        private readonly IStringLocalizer<GetCardDetailsQueryValidator> _localizer;

        public GetCardDetailsQueryValidator(ICardService cardService, IStringLocalizer<GetCardDetailsQueryValidator> localizer)
        {
            _cardService = cardService;
            _localizer = localizer;

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(_localizer["UserIdRequired"])
                .MustAsync(UserExists).WithMessage(_localizer["UserDoesNotExist"]);

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage(_localizer["CardNumberRequired"])
                .MustAsync(CardExists).WithMessage(_localizer["CardDoesNotExist"]);
        }

        private async Task<bool> UserExists(string userId, CancellationToken cancellationToken)
        {
            var userCards = await _cardService.GetCardDetailsAsync(userId, "");
            return userCards != null;
        }

        private async Task<bool> CardExists(GetCardDetailsQuery query, string cardNumber, CancellationToken cancellationToken)
        {
            var card = await _cardService.GetCardDetailsAsync(query.UserId, cardNumber);
            return card != null;
        }
    }
}