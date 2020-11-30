using Microsoft.AspNetCore.Routing;
using System;

namespace AspNetCore.LightweightApi.Extensions
{
    public static class RouteValueDictionaryExtensions
    {
        public static T? Get<T>(this RouteValueDictionary routeValues, string key)
        {
            var value = routeValues[key];
            if (value == null)
                return default;

            var t = typeof(T);
            var u = Nullable.GetUnderlyingType(t);
            return (T)Convert.ChangeType(value, u ?? t);
        }
    }
}
