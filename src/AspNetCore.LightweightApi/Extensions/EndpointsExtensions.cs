using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.LightweightApi.Extensions
{
    public static class EndpointsExtensions
    {
        public static void UseLightweightApi(this IEndpointRouteBuilder endpoints)
        {
            var endpointCollection = endpoints.ServiceProvider.GetRequiredService<EndpointCollection>();
            foreach (var endpoint in endpointCollection.Items)
            {
                var httpMethod = endpoint.Method.ToString().ToUpperInvariant();
                var requestDelegate = endpoint.RequestDelegate;
                endpoints.MapMethods(endpoint.Pattern, new[] { httpMethod }, requestDelegate);
            }
        }
    }
}
