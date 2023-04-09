using Newtonsoft.Json;

namespace Monobank;

/// <summary>
/// Represents a jar information.
/// </summary>
public class Jar
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Jar"/> class.
    /// </summary>
    /// <param name="id">the unique identifier of the jar.</param>
    /// <param name="sendId">the unique identifier of the https://send.monobank.ua/{sendId} service.</param>
    /// <param name="title">the name (the title) of the jar.</param>
    /// <param name="description">the description of the jar.</param>
    /// <param name="currencyCode">the currency code according to ISO 4217.</param>
    /// <param name="balance">the balance of the jar in the minimum units (coins, cents) of the jar currency.</param>
    /// <param name="goal"></param>
    [JsonConstructor]
    internal Jar(string id, string sendId, string title, string description, int currencyCode, long balance, long goal)
    {
        Id = id;
        SendId = sendId;
        Title = title;
        Description = description;
        CurrencyCode = currencyCode;
        Balance = balance;
        Goal = goal;
    }
        
    /// <summary>
    /// Gets the unique identifier of the jar.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; }

    /// <summary>
    /// Gets the unique identifier of the https://send.monobank.ua/{sendId} service.
    /// </summary>
    [JsonProperty("sendId")]
    public string SendId { get; }

    /// <summary>
    /// Gets the name (the title) of the jar.
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; }

    /// <summary>
    /// Gets the description of the jar.
    /// </summary>
    [JsonProperty("description")]
    public string Description { get; }

    /// <summary>
    /// Gets the currency code according to ISO 4217.
    /// </summary>
    [JsonProperty("currencyCode")]
    public int CurrencyCode { get; }

    /// <summary>
    /// Gets the balance of the jar in the minimum units (coins, cents) of the jar currency. 
    /// </summary>
    [JsonProperty("balance")]
    public long Balance { get; }

    /// <summary>
    /// Get the target amount to accumulate by the jar in the minimum units (coins, cents) of the jar currency.
    /// </summary>
    [JsonProperty("goal")]
    public long Goal { get; }
}