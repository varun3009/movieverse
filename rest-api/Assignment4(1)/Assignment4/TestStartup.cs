using System;
using System.Reflection;
using IMDBAPI;
using IMDBAPI.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace IMDB
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to addservices to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            // *** Do not add custom services here
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddAutoMapper(typeof(TestStartup));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (NotFoundException ex)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(ex.Message);
                }
                catch (InvalidInputException e)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync($"{e.Message}");

                }
                catch (Exception e)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(e.Message);
                }
            });
            app.UseHttpsRedirection();
            app.UseMvc();
            // *** Do not configure Authentication here
        }
    }
}
