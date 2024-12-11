namespace PaymentApi.Client.Security
{
    /// <summary>
    /// Interface for providing authorization headers for HTTP requests.
    /// Implementations of this interface can provide various authorization mechanisms (e.g., API Key, Basic Auth, JWT).
    /// </summary>
    public interface IAuthorizationProvider
    {
        /// <summary>
        /// Asynchronously retrieves the authorization headers to be included in an HTTP request.
        /// </summary>
        /// <returns>A dictionary of headers, typically including the Authorization header.</returns>
        Task<IDictionary<string, string>> GetAuthorizationHeadersAsync();
    }
}
