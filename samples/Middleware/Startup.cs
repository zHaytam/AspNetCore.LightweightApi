using AspNetCore.LightweightApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Middleware    
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<LoggerMiddleware>();
            services.UseLightweightApi();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<LoggerMiddleware>();
            app.Use(next => async context =>
            {
                context.Response.Headers.Add("test", "test");
                await next(context);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.UseLightweightApi();
            });
        }
    }
}
