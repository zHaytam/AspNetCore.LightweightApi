# AspNetCore.LightweightApi

A lightweight & performant framework for APIs, built on top ASP.NET Core (boilerplate)

## Example

```cs
[Endpoint("/users", Method = EndpointMethod.Post)]
public class CreateUserHandler : IEndpointHandler<NewUserDto, UserDto>
{
    public Task<UserDto> Handle(NewUserDto input, HttpContext context)
    {
        var newUser = new UserDto(1, input.Username, input.Email); // Save in db
        return Task.FromResult(newUser);
    }
}

public record NewUserDto(string Username, string Email);
```
