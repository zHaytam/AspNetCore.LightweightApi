using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AspNetCore.LightweightApi.UnitTests.Endpoints
{
    public class SimpleEndpointHandler : IEndpointHandler
    {
        public Task Handle(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
