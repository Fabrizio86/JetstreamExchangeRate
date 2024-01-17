namespace JetstreamExchangeRate.Interfaces;

/// <summary>
/// Interface to represent the operations of an exchange rate service.
/// Usually I would put this interface in it's own library project,
/// and shared between projects, avoiding circular reference or polluting reference pool.
/// </summary>
public interface IExchangeRateService
{
    /// <summary>
    /// Retrieves the exchange rate from a particular date.
    /// </summary>
    /// <param name="invoiceDate">The instance of <see cref="DateTime"/> used to retrieve the exchange rate.</param>
    /// <returns>Returns a <see cref="decimal"/> for the exchange rate.</returns>
    Task<decimal> GetExchangeRate(DateTime invoiceDate);
}
