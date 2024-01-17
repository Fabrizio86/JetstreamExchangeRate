namespace JetstreamExchangeRate.Core;

using JetstreamExchangeRate.Core.Interfaces;
using JetstreamExchangeRate.Models;

/// <summary>
/// Class implementing the invoice calculator interface
/// </summary>
public class InvoiceCalculator : IInvoiceCalculator
{
    /// <summary>
    /// Method to calculate invoice details based on input and exchange rate
    /// </summary>
    /// <param name="input">Input data containing <see cref="InvoiceInput"/> details.</param>
    /// <param name="exchangeRate">The exchange rate to be applied.</param>
    /// <returns>Returns an <see cref="InvoiceOutput"/> object with calculated details.</returns>
    public InvoiceOutput CalculateInvoiceDetails(InvoiceInput input, decimal exchangeRate)
    {
        var preTaxTotal = Math.Round(input.PreTaxAmount * exchangeRate, 2);
        var taxAmount = this.GetTaxRate(input.PaymentCurrency) * preTaxTotal;

        return new InvoiceOutput
        {
            PreTaxTotal = preTaxTotal,
            TaxAmount = taxAmount,
            GrandTotal = preTaxTotal + taxAmount,
            ExchangeRate = exchangeRate
        };
    }

    /// <summary>
    /// Retrieves the tax rates based on the currency. 
    /// </summary>
    /// <param name="paymentCurrency">The currency value representation.</param>
    /// <returns>Returns the tax rate.</returns>
    private decimal GetTaxRate(string paymentCurrency)
    {
        switch (paymentCurrency)
        {
            case "CAD":
                return 0.11m;// 11%
            case "USD":
                return 0.10m;// 10%
            default:
                return 0.09m;// 9% for EUR
        }
    }
}
