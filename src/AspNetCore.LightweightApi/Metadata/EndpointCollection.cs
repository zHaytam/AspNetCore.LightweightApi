using AspNetCore.LightweightApi.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCore.LightweightApi
{
    public class EndpointCollection
    {
        private static readonly Type _basicEndpointType = typeof(IEndpointHandler);
        private static readonly Type _endpointWithOutputType = typeof(IEndpointHandler<>);
        private static readonly Type _endpointWithInputAndOutputType = typeof(IEndpointHandler<,>);
        private readonly List<EndpointMetadata> _endpoints;

        public EndpointCollection()
        {
            _endpoints = new List<EndpointMetadata>();
        }

        public IReadOnlyCollection<EndpointMetadata> Items => _endpoints;

        public void Add(Type type) => _endpoints.Add(ExtractEndpointMetadata(type));

        public static bool IsHandler(Type type)
        {
            return _basicEndpointType.IsAssignableFrom(type) ||
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithOutputType) ||
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithInputAndOutputType);
        }

        private static EndpointHandlerType GetHandlerType(Type type)
        {
            if (_basicEndpointType.IsAssignableFrom(type))
                return EndpointHandlerType.Basic;

            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithOutputType))
                return EndpointHandlerType.WithOutput;

            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithInputAndOutputType))
                return EndpointHandlerType.WithInputAndOutput;

            throw new InvalidOperationException($"{type.Name} isn't an endpoint handler.");
        }

        private static EndpointMetadata ExtractEndpointMetadata(Type type)
        {
            var attr = type.GetCustomAttribute<EndpointAttribute>();
            var pattern = attr?.Pattern ?? "/";
            var method = attr?.Method ?? EndpointMethod.Get;
            return new EndpointMetadata(pattern, method, type, GetHandlerType(type));
        }
    }
}
