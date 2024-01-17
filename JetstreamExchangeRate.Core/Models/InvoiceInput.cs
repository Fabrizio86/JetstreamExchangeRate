namespace JetstreamExchangeRate.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Model representing the request payload.
/// </summary>
public class InvoiceInput
{
    /// <summary>
    /// Gets or sets the invoice date.
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Gets or sets the pre-tax amount.
    /// </summary>
    public decimal PreTaxAmount { get; set; }

    /// <summary>
    /// Gets or sets the payment currency.
    /// </summary>
    public string PaymentCurrency { get; set; }
}
