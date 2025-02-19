using FluentValidation;
using CardService.Api.Models;
using Microsoft.Extensions.Localization;

namespace CardService.Api.Validators
{
    public class CardRequestValidator : AbstractValidator<CardRequest>
    {
        public CardRequestValidator(IStringLocalizer<CardRequestValidator> localizer)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(localizer["UserIdRequired"]);
            RuleFor(x => x.CardNumber).NotEmpty().WithMessage(localizer["CardNumberRequired"]);
        }
    }
}