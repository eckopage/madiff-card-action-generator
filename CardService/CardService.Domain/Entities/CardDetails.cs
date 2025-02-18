using CardService.Domain.Enums;

namespace CardService.Domain.Entities
{
    /// <summary>
    /// Represents detailed information regarding a unique card.
    /// </summary>
    public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);
}