using CardService.Domain.Entities;

namespace CardService.Application.Interfaces
{
    public interface ICardService
    {
        /// <summary>
        /// Retrieves card details for a given user ID and card number.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>The card details if found; otherwise, null.</returns>
        Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber);
    }
}