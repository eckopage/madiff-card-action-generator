using AutoMapper;
using CardService.Application.Handlers;
using CardService.Application.Interfaces;
using CardService.Application.Models;
using CardService.Application.Queries;
using CardService.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Xunit;

namespace CardService.Tests.Handlers
{
    public class GetCardDetailsQueryHandlerTests
    {
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<GetCardDetailsQuery>> _validatorMock;
        private readonly GetCardDetailsQueryHandler _handler;

        public GetCardDetailsQueryHandlerTests()
        {
            _cardServiceMock = new Mock<ICardService>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<GetCardDetailsQuery>>();
            _handler = new GetCardDetailsQueryHandler(
                _cardServiceMock.Object,
                _mapperMock.Object,
                _validatorMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnCardDetails_WhenCardExists()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "123", CardNumber = "456" };
            
            var cardDetailsEntity = new CardDetails
            (
                CardNumber: "456",
                CardType: CardService.Domain.Enums.CardType.Credit,
                CardStatus: CardService.Domain.Enums.CardStatus.Active,
                IsPinSet: true
            );

            var cardDetailsDto = new CardDetailsDto
            {
                CardNumber = "456",
                CardType = "Credit",
                CardStatus = "Active",
                IsPinSet = true
            };
            
            _validatorMock.Setup(v => v.ValidateAsync(query, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());

            _cardServiceMock.Setup(s => s.GetCardDetailsAsync(It.IsAny<string>(), It.IsAny<string>()))
                            .ReturnsAsync(cardDetailsEntity);

            _mapperMock.Setup(m => m.Map<CardDetailsDto>(It.IsAny<CardDetails>()))
                       .Returns(cardDetailsDto);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.CardNumber.Should().Be("456");
            result.CardType.Should().Be("Credit");
            result.CardStatus.Should().Be("Active");
            result.IsPinSet.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenValidationFails()
        {
            // Arrange
            var query = new GetCardDetailsQuery { 
                UserId = "", 
                CardNumber = "" 
            };

            var validationResult = new ValidationResult(new[] { 
                new ValidationFailure("UserId", "UserId is required") 
            });
            
            _validatorMock.Setup(v => v.ValidateAsync(query, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(validationResult);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
    }
}
