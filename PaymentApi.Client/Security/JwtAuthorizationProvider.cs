namespace PaymentApi.Client.Security
{
    /// <summary>
    /// Provides JWT (JSON Web Token) authentication by adding a Bearer token to the Authorization header.
    /// </summary>
    public class JwtAuthorizationProvider : IAuthorizationProvider
    {
        private readonly string _token;  // The JWT token used for authorization

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthorizationProvider"/> class.
        /// </summary>
        /// <param name="token">The JWT token for authentication.</param>
        public JwtAuthorizationProvider(string token)
        {
            // Ensure the token is not null
            _token = token ?? throw new ArgumentNullException(nameof(token));
        }

        /// <summary>
        /// Asynchronously retrieves the authorization headers for JWT authentication.
        /// </summary>
        /// <returns>A dictionary of headers containing the Authorization header with the Bearer token.</returns>
        public Task<IDictionary<string, string>> GetAuthorizationHeadersAsync()
        {
            // Return a dictionary with the "Authorization" header containing the Bearer token
            return Task.FromResult<IDictionary<string, string>>(new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {_token}" }  // Bearer token format
            });
        }
    }
}
