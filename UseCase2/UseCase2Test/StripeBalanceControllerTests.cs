using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using UseCase2.Controllers;
using UseCase2.Models;

namespace UseCase2Test
{
    public class StripeBalanceControllerTests
    {

        [Fact]
        public void GetBalanceTransactions_ReturnsExpectedResult()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["Stripe:SecretKey"]).Returns("sk_test_51NaEHGHkhSKj2CFnZV7PIG1Lph0Hmhe7pqK4Cy4ZNY0Yw4XALcihYTbOfjacdXekjrAX80MW21PpY2CVzVQNx6Wh00TSchFGSq");

            var controller = new StripeBalanceController(mockConfiguration.Object);

            var result = controller.GetBalanceTransactions(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BalanceTransaction>>(okResult.Value);

            var transaction = returnValue.FirstOrDefault();
            Assert.NotNull(transaction);
            Assert.True(transaction.Amount >= 0);
            Assert.False(string.IsNullOrEmpty(transaction.Currency));
            Assert.False(string.IsNullOrEmpty(transaction.Status));
        }

        [Fact]
        public void GetBalance_ReturnsExpectedResult()
        {
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["Stripe:SecretKey"]).Returns("sk_test_51NaEHGHkhSKj2CFnZV7PIG1Lph0Hmhe7pqK4Cy4ZNY0Yw4XALcihYTbOfjacdXekjrAX80MW21PpY2CVzVQNx6Wh00TSchFGSq");

            var controller = new StripeBalanceController(mockConfiguration.Object);

            var result = controller.GetBalance();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Balance>(okResult.Value);

            Assert.NotNull(returnValue);
            Assert.True(returnValue.AvailableFunds.Count >= 0);
            Assert.True(returnValue.PendingFunds.Count >= 0);
            var pendingFunds = returnValue.PendingFunds.FirstOrDefault();
            Assert.False(string.IsNullOrEmpty(pendingFunds.Key));
            Assert.Equal("pln", pendingFunds.Key);
            Assert.True(pendingFunds.Value >= 0);
        }
    }
}