namespace PaymentApi.Client.Security
{
    /// <summary>
    /// Provides API key authorization by adding an API key to the HTTP request headers.
    /// </summary>
    public class ApiKeyAuthorizationProvider : IAuthorizationProvider
    {
        private readonly string _apiKey;  // The API key used for authorization
        private readonly string _headerName;  // The header name under which the API key will be added

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyAuthorizationProvider"/> class.
        /// </summary>
        /// <param name="apiKey">The API key for authorization.</param>
        /// <param name="headerName">The header name for the API key (default is "X-Api-Key").</param>
        public ApiKeyAuthorizationProvider(string apiKey, string headerName = "X-Api-Key")
        {
            // Ensure the API key is not null
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _headerName = headerName;  // Set the header name (default is "X-Api-Key")
        }

        /// <summary>
        /// Asynchronously retrieves the authorization headers containing the API key.
        /// </summary>
        /// <returns>A dictionary of headers with the API key included.</returns>
        public Task<IDictionary<string, string>> GetAuthorizationHeadersAsync()
        {
            // Return a dictionary with the header name and API key
            return Task.FromResult<IDictionary<string, string>>(new Dictionary<string, string>
            {
                { _headerName, _apiKey }  // Add the API key as a header
            });
        }
    }
}
