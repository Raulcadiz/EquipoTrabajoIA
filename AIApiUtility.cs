// AIApiUtility.cs
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public static class AIApiUtility
{
    private static readonly HttpClient httpClient = new HttpClient();

    public static async Task<string> GenerateResponseAsync(string prompt)
    {
        string apiUrl = "http://pass-gpt.nowtechai.com/api/v1/pass";
        string apiKey = "FG7W8mUbMIGbyCGKn4O5tT/UBbHOy9A6THkQD2NK9j80LyvCxv46fftuIlCK3bdyzAoLnZHecKUSeLpZXzmaZQ==";
        string timestamps = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Add("Key", apiKey);
        httpClient.DefaultRequestHeaders.Add("TimeStamps", timestamps);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Add("Accept-Charset", "UTF-8");
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Ktor client");
        httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");

        var requestBody = new
        {
            contents = new[]
            {
                new { role = "system", content = "" },
                new { role = "user", content = prompt }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return ExtractContentFromResponse(responseData);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error en la solicitud HTTP: {e.Message}");
            return "Lo siento, hubo un error al procesar la solicitud.";
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Error al procesar la respuesta JSON: {e.Message}");
            return "Lo siento, hubo un error al procesar la respuesta.";
        }
    }

    private static string ExtractContentFromResponse(string responseData)
    {
        var contentBuilder = new StringBuilder();
        var lines = responseData.Split(new[] { "data:" }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            try
            {
                var trimmedLine = line.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedLine))
                {
                    var data = JsonSerializer.Deserialize<JsonElement>(trimmedLine);
                    var content = data.GetProperty("content").GetString();
                    if (!string.IsNullOrEmpty(content))
                    {
                        contentBuilder.Append(content);
                    }
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error de análisis JSON: {ex.Message}");
            }
        }
        return contentBuilder.ToString();
    }
}
