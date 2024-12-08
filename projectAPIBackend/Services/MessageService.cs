using System.Text.Json;
using ProjectApiBackend.Models;

namespace ProjectApiBackend.Services;

public class MessageService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly ILogger<MessageService> _logger;

    public MessageService(HttpClient httpClient, IConfiguration configuration, ILogger<MessageService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiUrl = configuration["ApiSettings:MessengerApiUrl"] 
                  ?? throw new ArgumentNullException("API URL is not configured.");
    }

    public async Task<bool> SendMessageAsync(MessageData data)
    {
        try
        {
            HttpResponseMessage? response = await _httpClient.PostAsJsonAsync(_apiUrl, data);

            if (response.IsSuccessStatusCode)
            {
                PostResponseData? apiResponse = await response.Content.ReadFromJsonAsync<PostResponseData>();
                return apiResponse?.Status ?? false;
            }

            _logger.LogWarning("Failed to send message. Response Code: {StatusCode}", response.StatusCode);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending a message.");
            return false;
        }
    }

    public async Task<List<string>> GetMessagesAsync()
    {
        try
        {
            HttpResponseMessage? response = await _httpClient.GetAsync(_apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to retrieve messages. Response Code: {StatusCode}", response.StatusCode);
                return new List<string>();
            }

            string jsonResult = await response.Content.ReadAsStringAsync();
            
            List<string>? result = JsonSerializer.Deserialize<List<string>>(jsonResult);

            return result ?? new List<string>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving messages.");
            return new List<string>();
        }
    }
}
