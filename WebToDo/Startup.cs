using AutoMapper;
using Core.AutoMapper;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebToDo.Middleware;

namespace WebToDo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.ISSUER,

                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,

                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.Scan(scan =>
               scan.FromAssemblyDependencies(Assembly.Load("Infrastructure"))
                   .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Repository") &&
                        t.Namespace == "Infrastructure.Repositories"))
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());

            services.Scan(scan =>
               scan.FromAssemblyDependencies(Assembly.Load("Infrastructure"))
                   .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Service") &&
                        t.Namespace == "Infrastructure.Service"))
                   .AsImplementedInterfaces()
                   .WithScopedLifetime());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("version 1", new OpenApiInfo()
                {
                    Title = "WebToDo",
                    Version = "version 1"
                });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                          Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddDbContext<ToDoContext>(x => x.UseSqlServer(this.Configuration.GetConnectionString("default")));

            services.AddSingleton<IMyAuthorizationServiceSingelton, MyAuthorizationServiceSingelton>();
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(
                    new MapperProfile(
                        provider.GetService<IMyAuthorizationServiceSingelton>()
                        )
                    );
            }).CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/version 1/swagger.json", "Web api version 1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<ErrorMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("start");
                });
            });
        }
    }
}
