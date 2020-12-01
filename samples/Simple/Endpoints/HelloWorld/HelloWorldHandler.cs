using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Simple.Endpoints.GetUsers
{
    public class HelloWorldHandler : IEndpointHandler
    {
        public async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("Hello World!");
        }
    }
}
