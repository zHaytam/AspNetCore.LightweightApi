using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCore.LightweightApi
{
    public interface IEndpointHandler
    {
        public Task Handle(HttpContext context);
    }

    public interface IEndpointHandler<TOutput>
    {
        public Task<TOutput> Handle(HttpContext context);
    }

    public interface IEndpointHandler<TInput, TOutput>
    {
        public Task<TOutput> Handle(TInput input, HttpContext context);
    }
}
