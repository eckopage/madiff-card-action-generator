using MediatR;
using Microsoft.AspNetCore.Mvc;
using CardService.Api.Models;
using Microsoft.Extensions.Localization;
using CardService.Application.Queries;

namespace CardService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IStringLocalizer _localizer;

        public CardController(IMediator mediator, IStringLocalizer localizer)
        {
            _mediator = mediator;
            _localizer = localizer;
        }

        [HttpGet("{userId}/{cardNumber}")]
        public async Task<IActionResult> GetCardDetails([FromRoute] CardRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = new GetCardDetailsQuery
            {
                UserId = request.UserId,
                CardNumber = request.CardNumber
            };

            var result = await _mediator.Send(query);
            
            return result != null 
                ? Ok(result) 
                : NotFound(_localizer["CardNotFound"]);
        }
    }
}
