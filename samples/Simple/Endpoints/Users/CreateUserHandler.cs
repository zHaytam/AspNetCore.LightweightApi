using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Simple.Endpoints.Users
{
    [Endpoint("/users", Method = EndpointMethod.Post)]
    public class CreateUserHandler : IEndpointHandler.IWithRequest<NewUserDto>.IWithResponse<UserDto>
    {
        public Task<UserDto> Handle(NewUserDto input, HttpContext context)
        {
            var newUser = new UserDto(1, input.Username, input.Email);
            return Task.FromResult(newUser);
        }
    }

    public record NewUserDto(string Username, string Email);
}
