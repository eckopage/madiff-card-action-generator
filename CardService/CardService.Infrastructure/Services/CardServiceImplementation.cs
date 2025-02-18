using CardService.Application.Interfaces;
using CardService.Domain.Entities;
using CardService.Domain.Enums;

namespace CardService.Infrastructure.Services
{
    public class CardServiceImplementation : ICardService
    {
        /// <summary>
        /// Initializes the CardService with a simulated in-memory database of user cards.
        /// </summary>
        private readonly Dictionary<string, Dictionary<string, CardDetails>> _userCards;

        public CardServiceImplementation()
        {
            _userCards = CreateSampleUserCards();
        }

        public async Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber)
        {
            await Task.Delay(100);
            return _userCards.TryGetValue(userId, out var cards) && cards.TryGetValue(cardNumber, out var cardDetails) ? cardDetails : null;
        }

        /// <summary>
        /// Creates a sample set of user cards to simulate a database.
        /// </summary>
        /// <returns>A dictionary of user cards.</returns>
        private static Dictionary<string, Dictionary<string, CardDetails>> CreateSampleUserCards()
        {
            var userCards = new Dictionary<string, Dictionary<string, CardDetails>>();
            for (var i = 1; i <= 3; i++)
            {
                var cards = new Dictionary<string, CardDetails>();
                var cardIndex = 1;
                foreach (CardType cardType in Enum.GetValues(typeof(CardType)))
                {
                    foreach (CardStatus cardStatus in Enum.GetValues(typeof(CardStatus)))
                    {
                        var cardNumber = $"Card{i}{cardIndex}";
                        cards.Add(cardNumber,
                            new CardDetails(
                                CardNumber: cardNumber,
                                CardType: cardType,
                                CardStatus: cardStatus,
                                IsPinSet: cardIndex % 2 == 0));
                        cardIndex++;
                    }
                }

                var userId = $"User{i}";
                userCards.Add(userId, cards);
            }

            return userCards;
        }
    }
}