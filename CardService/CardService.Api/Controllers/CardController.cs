using CardService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CardService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}/{cardNumber}")]
        public async Task<IActionResult> GetCardDetails(string userId, string cardNumber)
        {
            var query = new GetCardDetailsQuery { 
                UserId = userId,
                 CardNumber = cardNumber
            };
            
            var result = await _mediator.Send(query);
            
            return result != null 
                ? Ok(result) 
                : NotFound("Card not found");
        }
    }
}
