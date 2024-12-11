using PaymentApi.Client.Clients;
using PaymentApi.Client.Models;

namespace PaymentApi.Client.Wrappers
{
    public class PaymentApiWrapper
    {
        private readonly ApiClient _apiClient;
        private readonly string _baseEndpoint;

        public PaymentApiWrapper(ApiClient apiClient, string baseEndpoint)
        {
            _apiClient = apiClient;
            _baseEndpoint = baseEndpoint;
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<IEnumerable<Payment>>(HttpMethod.Get, $"{_baseEndpoint}/payments", cancellationToken: cancellationToken)
                   ?? Enumerable.Empty<Payment>();
        }

        public async Task<Payment?> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<Payment>(HttpMethod.Get, $"{_baseEndpoint}/payments/{id}", cancellationToken: cancellationToken);
        }

        public async Task<Payment?> CreatePaymentAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<Payment>(HttpMethod.Post, $"{_baseEndpoint}/payments", payment, cancellationToken: cancellationToken);
        }

        public async Task<Payment?> UpdatePaymentAsync(Guid id, Payment payment, CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<Payment>(HttpMethod.Put, $"{_baseEndpoint}/payments/{id}", payment, cancellationToken: cancellationToken);
        }

        public async Task DeletePaymentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _apiClient.SendAsync(HttpMethod.Delete, $"{_baseEndpoint}/payments/{id}", cancellationToken: cancellationToken);
        }
    }
}