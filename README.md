
# Payment API Solution

This repository contains a sample solution for a Payment API with different types of security authentication mechanisms. It includes a set of three .NET 8 Web API projects, a reusable API client package, and a console application to test the API client.

### Solution Structure

-   **PaymentApi.Server.ApiKey**: A Web API using API Key Authentication for payment operations.
-   **PaymentApi.Server.BasicAuth**: A Web API using Basic Authentication for payment operations.
-   **PaymentApi.Server.JWTAuth**: A Web API using JWT (JSON Web Token) Authentication for payment operations.
-   **PaymentApi.Client**: A reusable, generic API client library that handles different authentication mechanisms (API Key, Basic Auth, JWT) and interacts with the Payment API endpoints.
-   **Payment.Console**: A console application to test the API client by making CRUD operations on the Payment API.

----------

### **Payment API Server Projects**

This solution contains three Web API projects, each demonstrating a different authentication method for securing the Payment API endpoints.

1.  **PaymentApi.Server.ApiKey**:
    
    -   This API project uses an API Key for authentication.
    -   Each request to the payment endpoints requires the `X-Api-Key` header.
    -   Example endpoints:
        -   `GET /payments` - Get all payments.
        -   `POST /payments` - Create a new payment.
        -   `PUT /payments/{id}` - Update a payment by ID.
        -   `DELETE /payments/{id}` - Delete a payment by ID.
2.  **PaymentApi.Server.BasicAuth**:
    
    -   This API project uses Basic Authentication.
    -   The `Authorization` header should contain the base64-encoded value of `username:password`.
    -   Example endpoints:
        -   `GET /payments` - Get all payments.
        -   `POST /payments` - Create a new payment.
        -   `PUT /payments/{id}` - Update a payment by ID.
        -   `DELETE /payments/{id}` - Delete a payment by ID.
3.  **PaymentApi.Server.JWTAuth**:
    
    -   This API project uses JWT (JSON Web Token) Authentication.
    -   A valid JWT token should be included in the `Authorization` header as `Bearer {token}`.
    -   Example endpoints:
        -   `GET /payments` - Get all payments.
        -   `POST /payments` - Create a new payment.
        -   `PUT /payments/{id}` - Update a payment by ID.
        -   `DELETE /payments/{id}` - Delete a payment by ID.

----------

### **Payment API Client Library (PaymentApi.Client)**

This package contains a reusable and generic API client library (`PaymentApi.Client`) that can be used to interact with the Payment API endpoints. It handles different types of authentication mechanisms (API Key, Basic Auth, and JWT) and abstracts away the complexity of making HTTP requests.

#### Key Features:

-   **Supports multiple authentication schemes**: API Key, Basic Auth, JWT.
-   **Generic and reusable**: Can be used across different APIs and endpoints with minimal configuration.
-   **Handles API calls**: Performs CRUD operations like GET, POST, PUT, DELETE on the Payment API.

##### Usage Example:

     // Create HttpClient
    var httpClient = new HttpClient { BaseAddress = new Uri("https://your-api-url.com") };
    
    // Choose the authorization provider (ApiKey, BasicAuth, JWT)
    IAuthorizationProvider authProvider = new ApiKeyAuthorizationProvider("your-api-key");
    
    // Create ApiClient with selected authorization provider
    var apiClient = new ApiClient(httpClient, authProvider);
    
    // Create PaymentApiWrapper with the ApiClient
    var paymentApiWrapper = new PaymentApiWrapper(apiClient, "/api/v1");
    
    // Example: Get all payments
    var payments = await paymentApiWrapper.GetAllPaymentsAsync();

### **Payment Console App (Payment.Console)**

The `Payment.Console` application demonstrates how to use the `PaymentApi.Client` to make API requests to the Payment API servers with different types of authentication. It is a simple console application that interacts with the Payment API endpoints, making CRUD requests.

#### Running the Console App:

1.  Build the solution to restore all dependencies.
2.  Open the `Payment.Console` project.
3.  Modify the authentication details (API Key, username/password, or JWT token) as per your API's requirements.
4.  Run the application to test the API client.

Example Console Output:

    Fetching payments...
    Payment ID: 123, Amount: 100.00
    Payment ID: 124, Amount: 150.00
    
### **Security Considerations**

-   **API Key**: Ensure your API key is kept secret. Do not hardcode it in your source code for production environments.
-   **Basic Authentication**: Use HTTPS to secure the transmission of the `username:password` pair, as it can be easily intercepted in plain text.
-   **JWT Authentication**: Secure the storage and transmission of JWT tokens. They should only be stored in secure locations such as `HttpOnly` cookies or secure storage mechanisms.

 ### **License**

This project is licensed under the MIT License - see the LICENSE file for details.

### **Contributions**

Feel free to fork this repository, open issues, or submit pull requests for enhancements, bug fixes, or improvements. Please make sure to follow best practices and write tests for new features.
Full Article https://dev.to/zrebhi/securing-a-net-api-c-api-key-basic-authentication-and-jwt-32e7
