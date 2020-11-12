using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AspNetCore.LightweightApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void UseLightweightApi(this IServiceCollection services)
            => UseLightweightApi(services, Assembly.GetCallingAssembly());

        public static void UseLightweightApi(this IServiceCollection services, params Assembly[] assemblies)
        {
            var endpointCollection = new EndpointCollection();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (!endpointCollection.IsHandler(type))
                        continue;

                    services.AddScoped(type);
                    endpointCollection.Add(type);
                }
            }

            services.AddSingleton(endpointCollection);
        }
    }
}
