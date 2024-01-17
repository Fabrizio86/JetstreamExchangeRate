namespace JetstreamExchangeRate.Services;

using JetstreamExchangeRate.Interfaces;
using JetstreamExchangeRate.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Service responsible for fetching exchange rates from the fixer.io API.
/// </summary>
public class FixerExchangeRateService : IExchangeRateService
{
    private readonly HttpClient httpClient;
    private readonly string fixerApiKey;
    private readonly string fixerApiUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="FixerExchangeRateService"/> class.
    /// </summary>
    /// <param name="httpClient">An instance of <see cref="HttpClient"/> used to make HTTP requests.</param>
    /// <param name="configuration">An instance of <see cref="IConfiguration"/> used to make HTTP requests.</param>
    public FixerExchangeRateService(HttpClient httpClient, IConfiguration configuration)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        this.fixerApiKey = configuration["FixerApiKey"] ?? throw new ArgumentNullException(nameof(configuration));
        this.fixerApiUrl = configuration["FixerApiUrl"] ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Retrieves the exchange rate based on the provided invoice date.
    /// </summary>
    /// <param name="invoiceDate">The date of the invoice, used to determine the applicable exchange rate.</param>
    /// <returns>The exchange rate for the specified date.</returns>
    public async Task<decimal> GetExchangeRate(DateTime invoiceDate)
    {
        // Construct the API URL with the Fixer.io API key and symbols (USD, CAD)
        var apiUrl = $"{fixerApiUrl}/historical?date={invoiceDate:yyyy-MM-dd}&access_key={this.fixerApiKey}";

        // Make an HTTP request to the Fixer.io API to retrieve exchange rates
        var response = await this.httpClient.GetStringAsync(apiUrl);

        // Parse the JSON response to extract exchange rates for USD and CAD
        var rates = JsonConvert.DeserializeObject<ExchangeRateResponse>(response);
        var rate = rates.Quotes["USDCAD"];
        
        // Choose the appropriate exchange rate based on the provided invoice date
        return rate;
    }
}
