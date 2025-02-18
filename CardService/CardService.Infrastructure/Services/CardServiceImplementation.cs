using CardService.Application.Interfaces;
using CardService.Domain.Entities;
using CardService.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardService.Infrastructure.Services
{
    public class CardServiceImplementation : ICardService
    {
        private readonly List<CardDetails> _cards = new()
        {
            new CardDetails { CardNumber = "1234", CardType = CardType.Credit, CardStatus = CardStatus.Active, IsPinSet = true },
            new CardDetails { CardNumber = "5678", CardType = CardType.Debit, CardStatus = CardStatus.Blocked, IsPinSet = false }
        };

        public Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber)
        {
            return Task.FromResult(_cards.FirstOrDefault(c => c.CardNumber == cardNumber));
        }
    }
}