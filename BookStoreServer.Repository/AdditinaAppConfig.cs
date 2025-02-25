using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Model.Contexts;
using BookStoreServer.Repository.AutoMapper;
using BookStoreServer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreServer.Repository
{
    public static class AdditinaAppConfig
    {
        //todo
        public static void ConfigureRepositoryServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(BookProfileDal));
            services.AddDbContext<BookStorContext>(options => options.UseSqlServer(config.GetConnectionString("BookStore")));
        }
    }
}
