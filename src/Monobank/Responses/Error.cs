using Newtonsoft.Json;

namespace Monobank;

internal class Error
{
    [JsonConstructor]
    public Error(string errCode, string errText)
    {
        Code = errCode;
        Text = errText;
    }
    
    [JsonProperty("errCode")]
    public string Code { get; }
    
    [JsonProperty("errText")]
    public string Text { get; }
}