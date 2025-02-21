using System.Reflection;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Repository;
using BookStoreServer.Repository.AutoMapper;
using BookStoreServer.Repository.Interfaces;
using BookStoreServer.Service.Cache;
using BookStoreServer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStoreServer.Entities.AppSettings;
using Microsoft.OpenApi.Models;
using BookStoreServer.CommonServices;
using Microsoft.AspNetCore.Builder;
using BookStoreServer.Service.Services;


namespace BookStoreServer.Server
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var connectionString = config.GetConnectionString("BookStore");

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
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
            builder.Services.AddDbContext<BookStorContext>(options=> options.UseSqlServer(connectionString));
            builder.Services.AddAutoMapper(typeof(BookProfileDal));
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    //ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            //builder.Services.AddHostedService<OldBooksCleanupService>();
            builder.Services.AddSignalR();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.Configure<JWTSettings>(config.GetSection("Jwt"));
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IBookHub, BookHubNotifier>();
            builder.Services.AddSingleton<SignalRClientService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            //builder.Host.UseSerilog((ctx, config) => config.ReadFrom.Configuration(ctx.Configuration));

          
            var app = builder.Build();

            app.UseRouting();

            app.MapControllers();
            app.MapHub<BookHub>("/book-hub");
            if (app.Environment.IsDevelopment())
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
           
      
            app.Run();
        }
    }
}
