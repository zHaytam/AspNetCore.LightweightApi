using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Endpoints.Misc
{
    [Endpoint("endpoints")]
    public class ListEndpointsHandler : IEndpointHandler.IWithResponse<IEnumerable<EndpointEntry>>
    {
        private readonly EndpointCollection _endpointCollection;

        public ListEndpointsHandler(EndpointCollection endpointCollection)
        {
            _endpointCollection = endpointCollection;
        }

        public Task<IEnumerable<EndpointEntry>> Handle(HttpContext context)
        {
            var endpoints = _endpointCollection.Items.Select(i => new EndpointEntry(i.Method.ToString(), i.Pattern));
            return Task.FromResult(endpoints);
        }
    }

    public record EndpointEntry(string Method, string Pattern);
}
