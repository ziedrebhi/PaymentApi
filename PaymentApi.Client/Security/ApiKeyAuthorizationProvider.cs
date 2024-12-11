namespace PaymentApi.Client.Security
{
    public class ApiKeyAuthorizationProvider : IAuthorizationProvider
    {
        private readonly string _apiKey;
        private readonly string _headerName;

        public ApiKeyAuthorizationProvider(string apiKey, string headerName = "X-Api-Key")
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _headerName = headerName;
        }

        public Task<IDictionary<string, string>> GetAuthorizationHeadersAsync()
        {
            return Task.FromResult<IDictionary<string, string>>(new Dictionary<string, string>
            {
                { _headerName, _apiKey }
            });
        }
    }
}