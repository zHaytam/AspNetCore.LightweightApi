using System;

namespace AspNetCore.LightweightApi
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EndpointAttribute : Attribute
    {
        public string Pattern { get; init; }
        public EndpointMethod Method { get; init; }

        public EndpointAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}
