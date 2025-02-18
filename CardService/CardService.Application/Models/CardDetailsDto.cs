namespace CardService.Application.Models
{
    /**
    * Represents a card with a unique identifier, a name, and a description.
    */
    public class CardDetailsDto
    {
        /*
        * The unique identifier of the card.
        */
        public required string CardNumber { get; set; }

        /*
        * The name of the card.
        */
        public required string CardType { get; set; }

        /*
        * The description of the card.
        */
        public required string CardStatus { get; set; }

        /*
        * The card's pin has set. 
        */
        public bool IsPinSet { get; set; }
    }
}