using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Repository;
using BookStoreServer.Service.Services;
using BookStoreServer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace BookStoreServer.Service
{
    public static class AdditinaAppConfig
    {
        public static void ConfigureServices(this IServiceCollection services , IConfiguration config)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<SuppliersClientService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddSingleton<IStoreHub, StoreHubNotifier>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "יש להזין את ה-JWT Token בפורמט הבא: Bearer {token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
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
            services.AddHostedService<HandleLowStockBooks>();
            services.ConfigureRepositoryServices(config);
        }
        public static void ConfigurApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();           
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-development");
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<StoreHub>("/order-hub");
            });
        }

    }
}
