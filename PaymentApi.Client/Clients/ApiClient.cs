using PaymentApi.Client.Security;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PaymentApi.Client.Clients
{
    /// <summary>
    /// A generic API client for interacting with secured payment APIs.
    /// Supports different authorization methods (e.g., API Key, Basic Auth, JWT).
    /// </summary>
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly IAuthorizationProvider _authorizationProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance used to send requests.</param>
        /// <param name="authorizationProvider">The provider responsible for adding authorization headers.</param>
        /// <param name="jsonOptions">Optional JSON serialization options.</param>

        public ApiClient(HttpClient httpClient, IAuthorizationProvider authorizationProvider, JsonSerializerOptions? jsonOptions = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _jsonOptions = jsonOptions ?? new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authorizationProvider = authorizationProvider ?? throw new ArgumentNullException(nameof(authorizationProvider));
        }

        /// <summary>
        /// Sends an HTTP request with the specified method and endpoint, and returns a deserialized response.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type.</typeparam>
        /// <param name="method">The HTTP method (e.g., GET, POST, etc.).</param>
        /// <param name="endpoint">The API endpoint to send the request to.</param>
        /// <param name="payload">Optional payload (used with POST, PUT).</param>
        /// <param name="headers">Optional additional headers.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
        /// <returns>The deserialized response of type <typeparamref name="TResponse"/>.</returns>

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
        /// <summary>
        /// Sends an HTTP request without expecting a specific response type.
        /// </summary>
        /// <param name="method">The HTTP method (e.g., GET, POST, etc.).</param>
        /// <param name="endpoint">The API endpoint to send the request to.</param>
        /// <param name="payload">Optional payload (used with POST, PUT).</param>
        /// <param name="headers">Optional additional headers.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the request.</param>

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