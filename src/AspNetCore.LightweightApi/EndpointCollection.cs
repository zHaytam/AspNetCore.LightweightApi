using System.Collections.Generic;

namespace AspNetCore.LightweightApi
{
    public class EndpointCollection
    {
        private readonly List<EndpointMetadata> _endpoints;

        public EndpointCollection()
        {
            _endpoints = new List<EndpointMetadata>();
        }

        public IReadOnlyCollection<EndpointMetadata> Items => _endpoints;

        public void Add(EndpointMetadata endpoint) => _endpoints.Add(endpoint);
    }
}
