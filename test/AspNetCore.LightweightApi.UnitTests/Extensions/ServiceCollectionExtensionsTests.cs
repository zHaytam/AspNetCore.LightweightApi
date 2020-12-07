using AspNetCore.LightweightApi.Extensions;
using AspNetCore.LightweightApi.UnitTests.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AspNetCore.LightweightApi.UnitTests.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void UseLightweightApi_ShouldRegisterEndpointsAsScoped()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.UseLightweightApi();

            // Assert
            var entry = serviceCollection[0];
            Assert.Equal(typeof(SimpleEndpointHandler), entry.ImplementationType);
            Assert.Equal(ServiceLifetime.Scoped, entry.Lifetime);
        }

        [Fact]
        public void UseLightweightApi_ShouldRegisterCollection()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            serviceCollection.UseLightweightApi();

            // Assert
            Assert.Contains(serviceCollection, s => s.ServiceType == typeof(EndpointCollection));
        }
    }
}
