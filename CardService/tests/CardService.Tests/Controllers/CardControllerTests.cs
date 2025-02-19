using CardService.Api.Controllers;
using CardService.Api.Models;
using CardService.Application.Models;
using CardService.Application.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace CardService.Tests.Controllers
{
    public class CardControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IStringLocalizer> _localizerMock;
        private readonly CardController _controller;

        public CardControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _localizerMock = new Mock<IStringLocalizer>();
            _controller = new CardController(_mediatorMock.Object, _localizerMock.Object);
        }

        [Fact]
        public async Task GetCardDetails_ShouldReturnNotFound_WhenCardDoesNotExist()
        {
            // Arrange
            var request = new CardRequest { UserId = "123", CardNumber = "456" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCardDetailsQuery>(), default))
                         .ReturnsAsync((CardDetailsDto)null);
            _localizerMock.Setup(l => l["CardNotFound"]).Returns(new LocalizedString("CardNotFound", "Card not found"));

            // Act
            var result = await _controller.GetCardDetails(request);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetCardDetails_ShouldReturnOk_WhenCardExists()
        {
            // Arrange
            var request = new CardRequest { UserId = "123", CardNumber = "456" };
            var cardDetails = new CardDetailsDto { CardNumber = "456", CardType = "Credit", CardStatus = "Active", IsPinSet = true };
            
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCardDetailsQuery>(), default))
                         .ReturnsAsync(cardDetails);

            // Act
            var result = await _controller.GetCardDetails(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetCardDetails_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("UserId", "UserId is required");
            var request = new CardRequest();

            // Act
            var result = await _controller.GetCardDetails(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}