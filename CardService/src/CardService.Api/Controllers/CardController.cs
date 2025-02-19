using MediatR;
using Microsoft.AspNetCore.Mvc;
using CardService.Api.Models;
using CardService.Application.Models;
using Microsoft.Extensions.Localization;
using CardService.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Retrieves details for a given card based on the user ID and card number.
        /// </summary>
        /// <param name="request">The card request containing user ID and card number.</param>
        /// <returns>Returns card details or an error message.</returns>
        /// <response code="200">Returns the card details.</response>
        /// <response code="400">If validation fails.</response>
        /// <response code="404">If the card is not found.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("{userId}/{cardNumber}")]
        [SwaggerOperation(Summary = "Get card details", Description = "Retrieves details for a given card based on the user ID and card number.")]
        [ProducesResponseType(typeof(CardDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCardDetails([FromRoute] CardRequest request)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = _localizer["InternalServerError"], details = ex.Message });
            }
        }
    }
}
