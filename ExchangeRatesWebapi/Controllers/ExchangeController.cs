using Exchange.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesWebapi.Controllers
{
    [ApiController]
    [Route("api/GetRates")]
    public class ExchangeController : Controller
    {
        private readonly ExchangeService exchangeService;

        public ExchangeController(ExchangeService exchangeService)
        {
            this.exchangeService = exchangeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRates()
        {
            try
            {
                await exchangeService.Parse();
                return Ok("Rates updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update rates: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetLastRowsByCurrency([FromQuery] string currencyName)
        {
            if (string.IsNullOrEmpty(currencyName))
            {
                return BadRequest("Currency name is required.");
            }

            var result = await exchangeService.PostRates(currencyName);

            return Ok(result);
        }
    }
}

