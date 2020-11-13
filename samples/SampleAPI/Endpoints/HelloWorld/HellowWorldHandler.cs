using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SampleAPI.Endpoints.GetUsers
{
    public class HellowWorldHandler : IEndpointHandler
    {
        public async Task Handle(HttpContext context)
        {
            await context.Response.WriteAsync("Hello World!");
        }
    }
}
