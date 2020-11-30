using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SampleAPI.Endpoints.Users
{
    [Endpoint("/users", Method = EndpointMethod.Patch)]
    public class UpdateUserHandler : IEndpointHandler.IWithRequest<UpdateUserDto>
    {
        public Task Handle(UpdateUserDto request, HttpContext context)
        {
            // Update in db
            return Task.CompletedTask;
        }
    }

    public record UpdateUserDto(string Username, string Email);
}
