using PaymentApi.Client.Security;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PaymentApi.Client.Clients
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IAuthorizationProvider _authorizationProvider;
        public ApiClient(HttpClient httpClient, IAuthorizationProvider authorizationProvider, JsonSerializerOptions? jsonOptions = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsonOptions = jsonOptions ?? new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authorizationProvider = authorizationProvider ?? throw new ArgumentNullException(nameof(authorizationProvider));
        }

        public async Task<TResponse?> SendAsync<TResponse>(
            HttpMethod method,
            string endpoint,
            object? payload = null,
            IDictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(method, endpoint);

            // Add authorization headers
            var authHeaders = await _authorizationProvider.GetAuthorizationHeadersAsync();
            foreach (var header in authHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            // Add payload if provided
            if (payload != null)
            {
                var jsonPayload = JsonSerializer.Serialize(payload, _jsonOptions);
                request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            }
            // Add additional custom headers if provided
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            await EnsureSuccessStatusCodeAsync(response);

            return response.Content.Headers.ContentLength > 0
                ? await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, cancellationToken)
                : default;
        }

        public async Task SendAsync(
            HttpMethod method,
            string endpoint,
            object? payload = null,
            IDictionary<string, string>? headers = null,
            CancellationToken cancellationToken = default)
        {
            await SendAsync<object>(method, endpoint, payload, headers, cancellationToken);
        }

        private static async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = response.Content != null
                    ? await response.Content.ReadAsStringAsync()
                    : string.Empty;

                throw new HttpRequestException($"Request failed with status {response.StatusCode}: {errorContent}");
            }
        }
    }
}