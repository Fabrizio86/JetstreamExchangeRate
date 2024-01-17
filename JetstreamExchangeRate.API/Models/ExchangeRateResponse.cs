namespace JetstreamExchangeRate.Models;

/// <summary>
/// Represents the response structure from the exchange rate API.
/// </summary>
public class ExchangeRateResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the API request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets the URL to the terms of service.
    /// </summary>
    public string Terms { get; set; }

    /// <summary>
    /// Gets or sets the URL to the privacy policy.
    /// </summary>
    public string Privacy { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the response includes historical exchange rates.
    /// </summary>
    public bool Historical { get; set; }

    /// <summary>
    /// Gets or sets the date for which the exchange rates are provided.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the response.
    /// </summary>
    public long Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the source currency for the exchange rates.
    /// </summary>
    public string Source { get; set; }

    /// <summary>
    /// Gets or sets the exchange rates for different currencies.
    /// The key is the currency code, and the value is the exchange rate.
    /// </summary>
    public Dictionary<string, decimal> Quotes { get; set; }
}

