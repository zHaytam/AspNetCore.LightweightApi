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
                var method = endpoint.Method.ToString().ToUpperInvariant();
                endpoints.MapMethods(endpoint.Pattern, new[] { method }, async context =>
                {
                    var handler = (IEndpointHandler)context.RequestServices.GetRequiredService(endpoint.Type);
                    await handler.Handle(context);
                });
            }
        }
    }
}
