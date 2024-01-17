namespace JetstreamExchangeRate.Core.Interfaces;

using JetstreamExchangeRate.Models;

/// <summary>
/// Interface defining the contract for invoice calculation.
/// Should live in it's own common library.
/// </summary>
public interface IInvoiceCalculator
{
    /// <summary>
    /// Method to calculate invoice details based on input and exchange rate
    /// </summary>
    /// <param name="input">The input containing invoice details.</param>
    /// <param name="exchangeRate">The exchange rate used in the calculation.</param>
    /// <returns>The calculated invoice details.</returns>
    InvoiceOutput CalculateInvoiceDetails(InvoiceInput input, decimal exchangeRate);
}
