using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SampleAPI.Endpoints.GetUsers
{
    public class HelloWorldEndpoint : IEndpointHandler
    {
        public async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("Hello World!");
        }
    }
}
