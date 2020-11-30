using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace AspNetCore.LightweightApi
{
    public class EndpointCollection
    {
        private static readonly Type _basicEndpointType = typeof(IEndpointHandler);
        private static readonly Type _endpointWithRequestType = typeof(IEndpointHandler.IWithRequest<>);
        private static readonly Type _endpointWithOutputType = typeof(IEndpointHandler.IWithResponse<>);
        private static readonly Type _endpointWithBothType = typeof(IEndpointHandler.IWithRequest<>.IWithResponse<>);
        private readonly List<EndpointMetadata> _endpoints;

        public EndpointCollection()
        {
            _endpoints = new List<EndpointMetadata>();
        }

        public IReadOnlyCollection<EndpointMetadata> Items => _endpoints;

        public void Add(Type type) => _endpoints.Add(ExtractEndpointMetadata(type));

        public static bool IsHandler(Type type)
        {
            return _basicEndpointType.IsAssignableFrom(type) ||
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithRequestType) ||
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithOutputType) ||
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithBothType);
        }

        private static EndpointMetadata ExtractEndpointMetadata(Type type)
        {
            var attr = type.GetCustomAttribute<EndpointAttribute>();
            var pattern = attr?.Pattern ?? "/";
            var method = attr?.Method ?? EndpointMethod.Get;
            return new EndpointMetadata(pattern, method, type, ExtractRequestDelegate(type));
        }

        private static RequestDelegate ExtractRequestDelegate(Type type)
        {
            if (_basicEndpointType.IsAssignableFrom(type))
                return GetRequestHandler(type);

            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithRequestType))
                return GetRequestHandlerOfEndpointWithRequest(type);

            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithOutputType))
                return GetRequestHandlerOfEndpointWithResponse(type);

            if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == _endpointWithBothType))
                return GetRequestHandlerOfEndpointWithBoth(type);

            throw new InvalidOperationException($"{type.Name} isn't an endpoint handler.");
        }

        private static RequestDelegate GetRequestHandler(Type type)
        {
            return async context =>
            {
                var handler = (IEndpointHandler)context.RequestServices.GetRequiredService(type);
                await handler.Handle(context);
            };
        }

        private static RequestDelegate GetRequestHandlerOfEndpointWithRequest(Type type)
        {
            var handleTask = GenerateHandlerLambdaWithRequest(type);
            return async context =>
            {
                var handler = context.RequestServices.GetRequiredService(type);
                var input = await context.Request.ReadFromJsonAsync(type.GetMethod("Handle")!.GetParameters()[0].ParameterType);
                await handleTask(handler, input, context);
            };
        }

        private static RequestDelegate GetRequestHandlerOfEndpointWithResponse(Type type)
        {
            var handleTask = GenerateHandlerLambdaWithResponse(type);
            return async context =>
            {
                var handler = context.RequestServices.GetRequiredService(type);
                var result = await handleTask(handler, context);
                await context.Response.WriteAsJsonAsync(result);
            };
        }

        private static RequestDelegate GetRequestHandlerOfEndpointWithBoth(Type type)
        {
            var handleTask = GenerateHandlerLambdaWithBoth(type);
            return async context =>
            {
                var handler = context.RequestServices.GetRequiredService(type);
                var input = await context.Request.ReadFromJsonAsync(type.GetMethod("Handle")!.GetParameters()[0].ParameterType);
                var result = await handleTask(handler, input, context);
                await context.Response.WriteAsJsonAsync(result);
            };
        }

        private static Func<object, object?, HttpContext, Task> GenerateHandlerLambdaWithRequest(Type type)
        {
            var handleMethod = type.GetMethod(nameof(IEndpointHandler.Handle))!;
            var inputType = handleMethod.GetParameters()[0].ParameterType;

            var handlerParam = Expression.Parameter(typeof(object), "handler");
            var inputParam = Expression.Parameter(typeof(object), "input");
            var contextParam = Expression.Parameter(typeof(HttpContext), "context");
            var instanceExpr = Expression.TypeAs(handlerParam, type);

            var castedInput = Expression.TypeAs(inputParam, inputType);
            var call = Expression.Call(instanceExpr, handleMethod, castedInput, contextParam);

            var lambda = Expression.Lambda<Func<object, object?, HttpContext, Task>>(call, handlerParam, inputParam, contextParam);
            return lambda.Compile();
        }

        private static Func<object, HttpContext, Task<object?>> GenerateHandlerLambdaWithResponse(Type type)
        {
            var handleMethod = type.GetMethod(nameof(IEndpointHandler.Handle))!;
            var outputType = handleMethod.ReturnType.GetGenericArguments();

            var handlerParam = Expression.Parameter(typeof(object), "handler");
            var contextParam = Expression.Parameter(typeof(HttpContext), "context");
            var instanceExpr = Expression.TypeAs(handlerParam, type);

            var call = Expression.Call(instanceExpr, handleMethod, contextParam);
            call = Expression.Call(typeof(EndpointCollection), nameof(ConvertTask), outputType, call);

            var lambda = Expression.Lambda<Func<object, HttpContext, Task<object?>>>(call, handlerParam, contextParam);
            return lambda.Compile();
        }

        private static Func<object, object?, HttpContext, Task<object?>> GenerateHandlerLambdaWithBoth(Type type)
        {
            var handleMethod = type.GetMethod(nameof(IEndpointHandler.Handle))!;
            var inputType = handleMethod.GetParameters()[0].ParameterType;
            var outputGenericArgs = handleMethod.ReturnType.GetGenericArguments();

            var handlerParam = Expression.Parameter(typeof(object), "handler");
            var inputParam = Expression.Parameter(typeof(object), "input");
            var contextParam = Expression.Parameter(typeof(HttpContext), "context");
            var instanceExpr = Expression.TypeAs(handlerParam, type);

            var castedInput = Expression.TypeAs(inputParam, inputType);
            var call = Expression.Call(instanceExpr, handleMethod, castedInput, contextParam);
            call = Expression.Call(typeof(EndpointCollection), nameof(ConvertTask), outputGenericArgs, call);

            var lambda = Expression.Lambda<Func<object, object?, HttpContext, Task<object?>>>(call, handlerParam, inputParam, contextParam);
            return lambda.Compile();
        }

        private static async Task<object?> ConvertTask<T>(Task<T> task) => await task;
    }
}
