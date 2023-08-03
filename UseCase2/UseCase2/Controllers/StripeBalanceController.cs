using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace UseCase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeBalanceController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StripeBalanceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public IActionResult GetBalance()
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

                var service = new BalanceService();
                var balance = service.Get();

                if (balance != null)
                {
                    var returnBalance = new Models.Balance()
                    {
                        AvailableFunds = balance.Available.GroupBy(x => x.Currency).ToDictionary(x => x.Key, x => (float) x.Sum(s => s.Amount) / 100),
                        PendingFunds = balance.Pending.GroupBy(x => x.Currency).ToDictionary(x => x.Key, x => (float) x.Sum(s => s.Amount) / 100)
                    };
                    return Ok(returnBalance);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (StripeException e)
            {
                switch (e.StripeError.Type)
                {
                    case "invalid_request_error":
                        return StatusCode(400, $"An invalid request occurred. {e}");
                    default:
                        return StatusCode(500, $"Another problem occurred, maybe unrelated to Stripe. {e}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("transactions")]
        public IActionResult GetBalanceTransactions(int limit = 100, string? startingAfter = null)
        {
            try
            {
                StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

                var service = new BalanceTransactionService();
                var options = new BalanceTransactionListOptions
                {
                    Limit = limit
                };

                if(!string.IsNullOrEmpty(startingAfter))
                    options.StartingAfter = startingAfter;

                var transactions = service.List(options);

                if (transactions != null)
                {
                    var mappedTransactions = transactions.Data.Select(t => new Models.BalanceTransaction
                    {
                        Amount = (float) t.Amount/100,
                        Currency = t.Currency,
                        Created = t.Created,
                        Status = t.Status,
                        Id = t.Id,
                        NettoAmount = (float) t.Net/100,
                        Type = t.Type
                    }).ToList();

                    return Ok(mappedTransactions);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (StripeException e)
            {
                switch (e.StripeError.Type)
                {
                    case "invalid_request_error":
                        return StatusCode(400, $"An invalid request occurred. {e}");
                    default:
                        return StatusCode(500, $"Another problem occurred, maybe unrelated to Stripe. {e}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
