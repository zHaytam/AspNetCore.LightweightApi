using AspNetCore.LightweightApi.Metadata;
using System;

namespace AspNetCore.LightweightApi
{
    public class EndpointMetadata
    {
        public string Pattern { get; set; }
        public EndpointMethod Method { get; set; }
        public Type Type { get; set; }
        public EndpointHandlerType HandlerType { get; set; }

        public EndpointMetadata(string pattern, EndpointMethod method, Type type, EndpointHandlerType handlerType)
        {
            Pattern = pattern;
            Method = method;
            Type = type;
            HandlerType = handlerType;
        }
    }
}
