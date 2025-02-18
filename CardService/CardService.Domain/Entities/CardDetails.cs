using CardService.Domain.Enums;

namespace CardService.Domain.Entities
{
    /// <summary>
    /// Represents detailed information regarding a unique card.
    /// </summary>
    public class CardDetails
    {
        /// <summary>
        /// Gets or sets card number. Card number uniquely identifies the card.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets type of the card. Card type can be Credit, Debit etc.
        /// </summary>
        public CardType CardType { get; set; }
        
        /// <summary>
        /// Gets or sets status of the card. 
        /// Card status can be ordered, inactive, active, restricted, blocked, expired, closed.
        /// </summary>   
        public CardStatus CardStatus { get; set; }

        /// <summary>
        /// Gets or sets state about pit that can be set or not
        /// </summary>
        public bool IsPinSet { get; set; }
    }
}