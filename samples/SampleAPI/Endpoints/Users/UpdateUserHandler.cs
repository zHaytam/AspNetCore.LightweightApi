using AspNetCore.LightweightApi;
using AspNetCore.LightweightApi.Extensions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SampleAPI.Endpoints.Users
{
    [Endpoint("/users/{id}", Method = EndpointMethod.Patch)]
    public class UpdateUserHandler : IEndpointHandler.IWithRequest<UpdateUserDto>
    {
        public Task Handle(UpdateUserDto request, HttpContext context)
        {
            var id = context.Request.RouteValues.Get<int>("id");
            // Update in db
            return Task.CompletedTask;
        }
    }

    public record UpdateUserDto(string Username, string Email);
}
