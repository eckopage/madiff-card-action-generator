using System.ComponentModel.DataAnnotations;

namespace CardService.Api.Models
{
    public class CardRequest
    {
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "CardNumber is required.")]
        public string CardNumber { get; set; }
    }
}