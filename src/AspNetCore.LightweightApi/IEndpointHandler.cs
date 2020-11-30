using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspNetCore.LightweightApi
{
    public interface IEndpointHandler
    {
        public interface IWithRequest<TRequest>
        {
            public interface IWithResponse<TResponse>
            {
                public Task<TResponse> Handle(TRequest request, HttpContext context);
            }

            public Task Handle(TRequest request, HttpContext context);
        }

        public interface IWithResponse<TResponse>
        {
            public Task<TResponse> Handle(HttpContext context);
        }

        public Task Handle(HttpContext context);
    }
}
