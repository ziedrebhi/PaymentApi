using System.Text;

namespace PaymentApi.Client.Security
{
    /// <summary>
    /// Provides basic authentication by encoding the username and password as a Base64 string
    /// and adding it to the HTTP request headers.
    /// </summary>
    public class BasicAuthorizationProvider : IAuthorizationProvider
    {
        private readonly string _username;  // The username for basic authentication
        private readonly string _password;  // The password for basic authentication

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicAuthorizationProvider"/> class.
        /// </summary>
        /// <param name="username">The username for basic authentication.</param>
        /// <param name="password">The password for basic authentication.</param>
        public BasicAuthorizationProvider(string username, string password)
        {
            // Ensure the username and password are not null
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        /// <summary>
        /// Asynchronously retrieves the authorization headers for basic authentication.
        /// </summary>
        /// <returns>A dictionary of headers containing the Authorization header with the Base64-encoded credentials.</returns>
        public Task<IDictionary<string, string>> GetAuthorizationHeadersAsync()
        {
            // Combine the username and password, then convert to Base64 encoding
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));

            // Return a dictionary with the "Authorization" header containing the encoded credentials
            return Task.FromResult<IDictionary<string, string>>(new Dictionary<string, string>
            {
                { "Authorization", $"Basic {credentials}" }  // Basic authentication header format
            });
        }
    }
}
