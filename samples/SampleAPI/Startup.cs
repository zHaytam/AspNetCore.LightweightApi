using AspNetCore.LightweightApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleAPI.Endpoints.Users.GetUsers;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseLightweightApi();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.UseLightweightApi();
            });
        }
    }
}
