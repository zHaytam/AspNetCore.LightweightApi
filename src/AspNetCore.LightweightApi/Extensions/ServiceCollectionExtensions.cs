using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AspNetCore.LightweightApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly Type _endpointType = typeof(IEndpointHandler);

        public static void UseLightweightApi(this IServiceCollection services)
            => UseLightweightApi(services, Assembly.GetCallingAssembly());

        public static void UseLightweightApi(this IServiceCollection services, params Assembly[] assemblies)
        {
            var endpointCollection = new EndpointCollection();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!_endpointType.IsAssignableFrom(type))
                        continue;

                    services.AddScoped(type);
                    endpointCollection.Add(ExtractEndpointMetadata(type));
                }
            }

            services.AddSingleton(endpointCollection);
        }

        private static EndpointMetadata ExtractEndpointMetadata(Type type)
        {
            var attr = type.GetCustomAttribute<EndpointAttribute>();
            var pattern = attr?.Pattern ?? "/";
            var method = attr?.Method ?? EndpointMethod.Get;
            return new EndpointMetadata(pattern, method, type);
        }
    }
}
