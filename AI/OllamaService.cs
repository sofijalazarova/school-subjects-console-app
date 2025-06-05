using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class OllamaService
{
    private readonly HttpClient _client;

    public OllamaService()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("http://localhost:11434");
    }

    public async Task<string> SummarizeAsync(string content)
    {
        var requestBody = new
        {
            model = "llama3.2:1b",
            prompt = $"Summarize the following text:\n\n{content}",
            stream = false
        };

        var json = JsonConvert.SerializeObject(requestBody);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("/api/generate", httpContent);
        var responseString = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseString);
        var root = doc.RootElement;

        if (root.TryGetProperty("response", out var summary))
        {
            return summary.GetString();
        }
        return "Failed to extract summary";
    }
 }