namespace JetstreamExchangeRate.Controllers;

using JetstreamExchangeRate.Core;
using JetstreamExchangeRate.Core.Interfaces;
using JetstreamExchangeRate.Interfaces;
using JetstreamExchangeRate.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller to retrieve <see cref="InvoiceOutput"/>
/// </summary>
[ApiController]
[Route("api/invoice")]
public class InvoiceController : ControllerBase
{
    /// <summary>
    /// Instance of <see cref="IExchangeRateService"/> used to retrieve rates.
    /// </summary>
    private readonly IExchangeRateService exchangeRateService;

    /// <summary>
    /// Instance of <see cref="InvoiceCalculator"/> used to retrieve computed rates.
    /// </summary>
    private readonly IInvoiceCalculator calculator;

    /// <summary>
    /// Initializes a new instance of <see cref="InvoiceController"/>.
    /// </summary>
    /// <param name="service">The injected instance of <see cref="IExchangeRateService"/>.</param>
    /// <param name="calculator">The injected instance of <see cref="InvoiceCalculator"/>.</param>
    public InvoiceController(IExchangeRateService service, IInvoiceCalculator calculator)
    {
        this.exchangeRateService = service;
        this.calculator = calculator;
    }

    /// <summary>
    /// Calculates invoice details based on the provided input.
    /// </summary>
    /// <param name="input">Input data containing the invoice details of <see cref="InvoiceInput"/>.</param>
    /// <returns>Returns the calculated invoice details of <see cref="InvoiceOutput"/>.</returns>
    [HttpPost("calculate")]
    public async Task<IActionResult> CalculateInvoice([FromBody] InvoiceInput input)
    {
        try
        {
            // Validate input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Fetch exchange rates using the injected service
            var exchangeRate = await this.exchangeRateService.GetExchangeRate(input.InvoiceDate);
            var result = this.calculator.CalculateInvoiceDetails(input, exchangeRate);

            return Ok(result);
        }
        catch (Exception ex)
        {
            // Handle exceptions and log them
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
