# AspNetCore.LightweightApi

A lightweight & performant framework for APIs, built on top ASP.NET Core (boilerplate)

## Example

```cs
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
```
