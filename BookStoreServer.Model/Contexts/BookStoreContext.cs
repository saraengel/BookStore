using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreServer.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Model.Contexts
{
    public class BookStorContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public BookStorContext(DbContextOptions options) : base(options)
        {
           
        }
    }
}
