using CardService.Application.Interfaces;
using CardService.Application.Queries;
using CardService.Application.Validators;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;
using CardService.Domain.Entities;
using CardService.Domain.Enums;

namespace CardService.Tests.Validators
{
    public class GetCardDetailsQueryValidatorTests
    {
        private readonly Mock<ICardService> _cardServiceMock;
        private readonly Mock<IStringLocalizer<GetCardDetailsQueryValidator>> _localizerMock;
        private readonly GetCardDetailsQueryValidator _validator;

        public GetCardDetailsQueryValidatorTests()
        {
            _cardServiceMock = new Mock<ICardService>();
            _localizerMock = new Mock<IStringLocalizer<GetCardDetailsQueryValidator>>();

            // Setup localized messages
            _localizerMock.Setup(l => l["UserIdRequired"]).Returns(new LocalizedString("UserIdRequired", "User ID is required."));
            _localizerMock.Setup(l => l["UserDoesNotExist"]).Returns(new LocalizedString("UserDoesNotExist", "User does not exist."));
            _localizerMock.Setup(l => l["CardNumberRequired"]).Returns(new LocalizedString("CardNumberRequired", "Card number is required."));
            _localizerMock.Setup(l => l["CardDoesNotExist"]).Returns(new LocalizedString("CardDoesNotExist", "Card does not exist for this user."));

            _validator = new GetCardDetailsQueryValidator(_cardServiceMock.Object, _localizerMock.Object);
        }

        [Fact]
        public async Task ShouldPassValidation_WhenValidRequest()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "123", CardNumber = "456" };

            _cardServiceMock.Setup(s => s.GetCardDetailsAsync(query.UserId, ""))
                            .ReturnsAsync(new CardDetails("456", CardType.Credit, CardStatus.Active, true));

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task ShouldFailValidation_WhenUserIdIsEmpty()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "", CardNumber = "456" };

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.UserId)
                .WithErrorMessage("User ID is required.");
        }

        [Fact]
        public async Task ShouldFailValidation_WhenCardNumberIsEmpty()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "123", CardNumber = "" };

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.CardNumber)
                .WithErrorMessage("Card number is required.");
        }

        [Fact]
        public async Task ShouldFailValidation_WhenUserDoesNotExist()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "999", CardNumber = "456" };

            _cardServiceMock.Setup(s => s.GetCardDetailsAsync(query.UserId, ""))
                            .ReturnsAsync((CardDetails?)null);

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.UserId)
                .WithErrorMessage("User does not exist.");
        }

        [Fact]
        public async Task ShouldFailValidation_WhenCardDoesNotExistForUser()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "123", CardNumber = "999" };

            _cardServiceMock.Setup(s => s.GetCardDetailsAsync(query.UserId, query.CardNumber))
                            .ReturnsAsync((CardDetails?)null);

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.CardNumber)
                .WithErrorMessage("Card does not exist for this user.");
        }

        [Fact]
        public async Task ShouldFailValidation_WhenLocalizationKeysAreMissing()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "", CardNumber = "" };

            // Simulate missing localization key
            _localizerMock.Setup(l => l["UserIdRequired"]).Returns(new LocalizedString("UserIdRequired", ""));

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.UserId);
            result.ShouldHaveValidationErrorFor(q => q.CardNumber);
        }

        [Fact]
        public async Task ShouldFailValidation_WhenCardNumberIsInvalidFormat()
        {
            // Arrange
            var query = new GetCardDetailsQuery { UserId = "123", CardNumber = "XYZ" };

            _cardServiceMock.Setup(s => s.GetCardDetailsAsync(query.UserId, query.CardNumber))
                            .ReturnsAsync((CardDetails?)null);

            // Act
            var result = await _validator.TestValidateAsync(query);

            // Assert
            result.ShouldHaveValidationErrorFor(q => q.CardNumber)
                .WithErrorMessage("Card does not exist for this user.");
        }
    }
}
