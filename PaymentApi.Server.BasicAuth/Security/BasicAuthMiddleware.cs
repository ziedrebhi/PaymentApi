using System.Net.Http.Headers;
using System.Text;

namespace PaymentApi.Server.BasicAuth.Security
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Response.StatusCode = 401;
                context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"PaymentApi\"";
                await context.Response.WriteAsync("Authorization header is missing.");
                return;
            }

            var authHeader = AuthenticationHeaderValue.Parse(authorizationHeader);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            if (credentials.Length != 2 || credentials[0] != "test" || credentials[1] != "test")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            await _next(context);
        }
    }

    public static class BasicAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthMiddleware>();
        }
    }
}
