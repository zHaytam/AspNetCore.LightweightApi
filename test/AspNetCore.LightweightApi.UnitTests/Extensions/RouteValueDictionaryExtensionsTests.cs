using AspNetCore.LightweightApi.Extensions;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace AspNetCore.LightweightApi.UnitTests.Extensions
{
    public class RouteValueDictionaryExtensionsTests
    {
        [Fact]
        public void Get_ShouldReturnDefaultValue_WhenKeyDoesntExist()
        {
            // Arrange
            var routeValues = new RouteValueDictionary();

            // Act
            var value = routeValues.Get<string>("key");

            // Assert
            Assert.Null(value);
        }

        [Fact]
        public void Get_ShouldHandlePrimitives()
        {

            // Arrange
            var routeValues = new RouteValueDictionary();
            routeValues.Add("key", "2");

            // Act
            var value = routeValues.Get<int>("key");

            // Assert
            Assert.Equal(2, value);
        }

        [Fact]
        public void Get_ShouldHandleNullable()
        {
            // Arrange
            var routeValues = new RouteValueDictionary();
            routeValues.Add("key", "1");

            // Act
            var value = routeValues.Get<int?>("key");

            // Assert
            Assert.True(value.HasValue);
            Assert.Equal(1, value);
        }
    }
}
