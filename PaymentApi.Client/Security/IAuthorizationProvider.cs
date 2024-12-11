namespace PaymentApi.Client.Security
{
    public interface IAuthorizationProvider
    {
        Task<IDictionary<string, string>> GetAuthorizationHeadersAsync();
    }
}
