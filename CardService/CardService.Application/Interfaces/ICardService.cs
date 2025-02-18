using CardService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardService.Application.Interfaces
{
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetailsAsync(string userId, string cardNumber);
    }
}