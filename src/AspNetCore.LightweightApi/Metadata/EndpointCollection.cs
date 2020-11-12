using System;
using System.Collections.Generic;
using System.Reflection;

namespace AspNetCore.LightweightApi
{
    public class EndpointCollection
    {
        private static readonly Type _endpointType = typeof(IEndpointHandler);
        private readonly List<EndpointMetadata> _endpoints;

        public EndpointCollection()
        {
            _endpoints = new List<EndpointMetadata>();
        }

        public IReadOnlyCollection<EndpointMetadata> Items => _endpoints;

        public bool IsHandler(Type type)
        {
            return _endpointType.IsAssignableFrom(type);
        }

        public void Add(Type type) => _endpoints.Add(ExtractEndpointMetadata(type));

        private static EndpointMetadata ExtractEndpointMetadata(Type type)
        {
            var attr = type.GetCustomAttribute<EndpointAttribute>();
            var pattern = attr?.Pattern ?? "/";
            var method = attr?.Method ?? EndpointMethod.Get;
            return new EndpointMetadata(pattern, method, type);
        }
    }
}
