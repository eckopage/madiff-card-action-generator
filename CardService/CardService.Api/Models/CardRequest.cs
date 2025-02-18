using System.ComponentModel.DataAnnotations;

namespace CardService.Api.Models
{
    public class CardRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "CardNumber is required.")]
        public required string CardNumber { get; set; }
    }
}