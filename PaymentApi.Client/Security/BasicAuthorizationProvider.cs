using System.Text;

namespace PaymentApi.Client.Security
{
    public class BasicAuthorizationProvider : IAuthorizationProvider
    {
        private readonly string _username;
        private readonly string _password;

        public BasicAuthorizationProvider(string username, string password)
        {
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public Task<IDictionary<string, string>> GetAuthorizationHeadersAsync()
        {
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));
            return Task.FromResult<IDictionary<string, string>>(new Dictionary<string, string>
            {
                { "Authorization", $"Basic {credentials}" }
            });
        }
    }
}