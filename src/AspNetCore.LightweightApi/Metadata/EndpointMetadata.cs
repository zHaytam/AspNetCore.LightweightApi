using Microsoft.AspNetCore.Http;
using System;

namespace AspNetCore.LightweightApi
{
    public record EndpointMetadata(string Pattern,
        EndpointMethod Method,
        Type HandlerType,
        RequestDelegate RequestDelegate,
        bool RequiresAuth,
        string[] AuthPolicies);
}
