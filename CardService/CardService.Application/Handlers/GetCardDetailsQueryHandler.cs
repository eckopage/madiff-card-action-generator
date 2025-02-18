using AutoMapper;
using CardService.Application.Interfaces;
using CardService.Application.Models;
using CardService.Application.Queries;
using FluentValidation;
using MediatR;

namespace CardService.Application.Handlers
{
    public class GetCardDetailsQueryHandler : IRequestHandler<GetCardDetailsQuery, CardDetailsDto?>
    {
        private readonly ICardService _cardService;
        private readonly IMapper _mapper;
        private readonly IValidator<GetCardDetailsQuery> _validator;
        
        public GetCardDetailsQueryHandler(ICardService cardService, IMapper mapper, IValidator<GetCardDetailsQuery> validator)
        {
            _cardService = cardService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<CardDetailsDto?> Handle(GetCardDetailsQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return null;
            }

            var card = await _cardService.GetCardDetailsAsync(request.UserId, request.CardNumber);
            return card != null ? _mapper.Map<CardDetailsDto>(card) : null;
        }
    }
}