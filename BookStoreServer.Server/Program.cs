
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStoreServer.Entities.AppSettings;
using BookStoreServer.Service;
using BookStoreServer.Model.Contexts;
using BookStoreServer.CommonServices;



namespace BookStoreServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://0.0.0.0:5000");
            var config = builder.Configuration;

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.Configure<JWTSettings>(config.GetSection("Jwt")); 
            builder.Services.Configure<EmailSettings>(config.GetSection("EmailSettings")); 
            builder.Services.AddSingleton<EventAggregator>();
            builder.Services.ConfigureServices(config);

            var app = builder.Build();

            // Apply database migrations
            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<BookStorContext>();
            //    db.Database.Migrate();
            //}

            app.ConfigurApp(app.Environment);
            app.Run();
        }
    }
}
