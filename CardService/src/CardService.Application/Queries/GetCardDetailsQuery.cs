using MediatR;
using CardService.Application.Models;

namespace CardService.Application.Queries
{
    public class GetCardDetailsQuery : IRequest<CardDetailsDto>
    {
        public required string UserId { get; set; }
        public required string CardNumber { get; set; }
    }
}