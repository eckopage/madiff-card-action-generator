using MediatR;
using CardService.Application.Models;

namespace CardService.Application.Queries
{
    public class GetCardDetailsQuery : IRequest<CardDetailsDto>
    {
        public string UserId { get; set; }
        public string CardNumber { get; set; }
    }
}
