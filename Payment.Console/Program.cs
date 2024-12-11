using PaymentApi.Client.Clients;
using PaymentApi.Client.Security;
using PaymentApi.Client.Wrappers;

// Your API base URL and API key
string baseUrl = "https://localhost:44322";
string apiKey = "SuperSecretApiKey12345";

// Create HttpClient
var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };

// Choose the authorization provider based on your needs
IAuthorizationProvider authProvider = new ApiKeyAuthorizationProvider(apiKey);

//IAuthorizationProvider authProvider = new BasicAuthorizationProvider("test", "test");
//IAuthorizationProvider authProvider = new JwtAuthorizationProvider("your-jwt-token");

// Create ApiClient with the authorization provider
var apiClient = new ApiClient(httpClient, authProvider);

// Create PaymentApiWrapper with the API client and base endpoint
var paymentApiWrapper = new PaymentApiWrapper(apiClient, "https://localhost:44322/api");

// Example: Get all payments
var payments = await paymentApiWrapper.GetAllPaymentsAsync();
foreach (var payment in payments)
{
    Console.WriteLine($"Payment ID: {payment.Id}, Amount: {payment.Amount}");
}