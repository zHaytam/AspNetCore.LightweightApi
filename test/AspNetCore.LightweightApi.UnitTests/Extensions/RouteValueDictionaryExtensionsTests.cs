using AspNetCore.LightweightApi.Extensions;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace AspNetCore.LightweightApi.UnitTests.Extensions
{
    public class RouteValueDictionaryExtensionsTests
    {
        [Fact]
        public void Get_ShouldReturnNull_WhenKeyDoesntExist()
        {
            // Arrange
            var routeValues = new RouteValueDictionary();

            // Act
            var value = routeValues.Get<string>("key");

            // Assert
            Assert.Null(value);
        }
    }
}
