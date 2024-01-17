namespace JetstreamExchangeRate.Models;

/// <summary>
/// Model representing the invoice output data.
/// </summary>
public class InvoiceOutput
{
    /// <summary>
    /// Gets or sets the pre-tax total amount.
    /// </summary>
    public decimal PreTaxTotal { get; set; }

    /// <summary>
    /// Gets or sets the tax amount value.
    /// </summary>
    public decimal TaxAmount { get; set; }

    /// <summary>
    /// Gets or sets the grand total amount.
    /// </summary>
    public decimal GrandTotal { get; set; }

    /// <summary>
    /// Gets or sets the exchange rate value.
    /// </summary>
    public decimal ExchangeRate { get; set; }
}
