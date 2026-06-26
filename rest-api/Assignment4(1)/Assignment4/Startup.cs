using IMDBAPI.Repository;
using IMDBAPI.Repository.Interfaces;
using IMDBAPI.Services;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Session;
using System.Security.Policy;
using Microsoft.AspNetCore.Http;
using System.Net.Sockets;
using System;
using System.Text.Json.Serialization;
using IMDBAPI.Exceptions;
using StackExchange.Redis;
using IMDBAPI.Models.DBModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace IMDBAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch(NotFoundException ex)
                {
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync(ex.Message);
                }
                catch(InvalidInputException e)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync($"{e.Message}");

                }
                catch(Exception e)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(e.Message);
                }
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
                    
            });
            

        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                     .WithOrigins("http://localhost:8080", "http://localhost:44349")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                     //.AllowCredentials());
                     );
            });

            var jwtKey = Configuration["Jwt:Key"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

            services.AddAuthorization();

            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(Startup));
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<RabbitMQConnection>(Configuration.GetSection("RabbitMQ"));
            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer.Connect(Configuration["Redis:ConnectionString"])
            );
            services.AddSingleton<IProducerRepository,ProducerRepository>();
            services.AddSingleton<IActorRepository, ActorRepository>();
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<IGenreRepository, GenreRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IUploadRepository, UploadRepository>();
            services.AddSingleton<IProducerService, ProducerService>();
            services.AddSingleton<IUploadService, UploadService>();
            services.AddSingleton<IActorService, ActorService>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IGenreService, GenreService>();
            services.AddSingleton<IReviewService, ReviewService>();
            services.AddSingleton(typeof(IMessageService<>),typeof(MessageService<>));
            services.AddHostedService<ImageBackgroundService>();
            services.AddSingleton<JwtService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
