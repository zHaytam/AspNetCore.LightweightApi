using Microsoft.AspNetCore.Http;
using System;

namespace AspNetCore.LightweightApi
{
    public class EndpointMetadata
    {
        public string Pattern { get; set; }
        public EndpointMethod Method { get; set; }
        public Type HandlerType { get; set; }
        public RequestDelegate RequestDelegate { get; set; }

        public EndpointMetadata(string pattern, EndpointMethod method, Type handlerType, RequestDelegate requestDelegate)
        {
            Pattern = pattern;
            Method = method;
            HandlerType = handlerType;
            RequestDelegate = requestDelegate;
        }
    }
}
