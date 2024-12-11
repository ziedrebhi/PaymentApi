using PaymentApi.Client.Clients;
using PaymentApi.Client.Models;

namespace PaymentApi.Client.Wrappers
{
    /// <summary>
    /// Wrapper class to interact with the Payment API, providing CRUD operations for payments.
    /// </summary>
    public class PaymentApiWrapper
    {
        private readonly ApiClient _apiClient;  // The ApiClient instance used to send HTTP requests
        private readonly string _baseEndpoint;  // The base URL for the payment API

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentApiWrapper"/> class.
        /// </summary>
        /// <param name="apiClient">The ApiClient instance used for making API requests.</param>
        /// <param name="baseEndpoint">The base URL endpoint for the payment API.</param>
        public PaymentApiWrapper(ApiClient apiClient, string baseEndpoint)
        {
            _apiClient = apiClient;  // Assign the provided ApiClient
            _baseEndpoint = baseEndpoint;  // Assign the provided base endpoint
        }

        /// <summary>
        /// Asynchronously retrieves all payments from the API.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the operation (optional).</param>
        /// <returns>A list of all payments.</returns>
        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<IEnumerable<Payment>>(HttpMethod.Get, $"{_baseEndpoint}/payments", cancellationToken: cancellationToken)
                   ?? Enumerable.Empty<Payment>();
        }

        /// <summary>
        /// Asynchronously retrieves a specific payment by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment to retrieve.</param>
        /// <param name="cancellationToken">A token to cancel the operation (optional).</param>
        /// <returns>The payment with the specified ID, or null if not found.</returns>
        public async Task<Payment?> GetPaymentByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<Payment>(HttpMethod.Get, $"{_baseEndpoint}/payments/{id}", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Asynchronously creates a new payment.
        /// </summary>
        /// <param name="payment">The payment details to create.</param>
        /// <param name="cancellationToken">A token to cancel the operation (optional).</param>
        /// <returns>The created payment object.</returns>
        public async Task<Payment?> CreatePaymentAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<Payment>(HttpMethod.Post, $"{_baseEndpoint}/payments", payment, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Asynchronously updates an existing payment.
        /// </summary>
        /// <param name="id">The ID of the payment to update.</param>
        /// <param name="payment">The updated payment details.</param>
        /// <param name="cancellationToken">A token to cancel the operation (optional).</param>
        /// <returns>The updated payment object.</returns>
        public async Task<Payment?> UpdatePaymentAsync(Guid id, Payment payment, CancellationToken cancellationToken = default)
        {
            return await _apiClient.SendAsync<Payment>(HttpMethod.Put, $"{_baseEndpoint}/payments/{id}", payment, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Asynchronously deletes a payment by its ID.
        /// </summary>
        /// <param name="id">The ID of the payment to delete.</param>
        /// <param name="cancellationToken">A token to cancel the operation (optional).</param>
        public async Task DeletePaymentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _apiClient.SendAsync(HttpMethod.Delete, $"{_baseEndpoint}/payments/{id}", cancellationToken: cancellationToken);
        }
    }
}
