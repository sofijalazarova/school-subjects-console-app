using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

public class OllamaService
{
    private readonly HttpClient _client;

    public OllamaService()
    {
        _client = new HttpClient();

        string protocol = Environment.GetEnvironmentVariable("OLLAMA_PROTOCOL");
        string host = Environment.GetEnvironmentVariable("OLLAMA_HOST");
        string port = Environment.GetEnvironmentVariable("OLLAMA_PORT");

        _client.BaseAddress = new Uri($"{protocol}://{host}:{port}");
    }

    public async Task<string> SummarizeAsync(string content)
    {
        string model = Environment.GetEnvironmentVariable("OLLAMA_MODEL");
        var requestBody = new
        {
            model = $"{model}",
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