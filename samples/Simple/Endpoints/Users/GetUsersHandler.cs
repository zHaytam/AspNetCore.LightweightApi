using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Simple.Endpoints.Users
{
    [Endpoint("/users")]
    public class GetUsersHandler : IEndpointHandler.IWithResponse<UserDto[]>
    {
        public Task<UserDto[]> Handle(HttpContext context)
        {
            var users = new[]
            {
                new UserDto(1, "user1", "user1@gmail.com"),
                new UserDto(2, "user2", "user2@gmail.com"),
                new UserDto(3, "user3", "user3@gmail.com")
            };

            return Task.FromResult(users);
        }
    }
}
