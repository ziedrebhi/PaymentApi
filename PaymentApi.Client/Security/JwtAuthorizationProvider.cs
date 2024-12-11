namespace PaymentApi.Client.Security
{
    public class JwtAuthorizationProvider : IAuthorizationProvider
    {
        private readonly string _token;

        public JwtAuthorizationProvider(string token)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public Task<IDictionary<string, string>> GetAuthorizationHeadersAsync()
        {
            return Task.FromResult<IDictionary<string, string>>(new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {_token}" }
            });
        }
    }
}