﻿using AspNetCore.LightweightApi;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SampleAPI.Endpoints.Users
{
    [Endpoint("/users", Method = EndpointMethod.Post)]
    public class CreateUserHandler : IEndpointHandler<NewUserDto, UserDto>
    {
        public Task<UserDto> Handle(NewUserDto input, HttpContext context)
        {
            var newUser = new UserDto(1, input.Username, input.Email);
            return Task.FromResult(newUser);
        }
    }

    public record NewUserDto(string Username, string Email);
}
