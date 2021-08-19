using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider){
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any()){
                    return;
                }
                context.Genres.AddRange(
                    new Genre{Name = "Personal Growth"},
                    new Genre{Name = "Science Fiction"},
                    new Genre{Name = "Romance"}
                );
                context.Books.AddRange(
                    new Book{Title="Lean Startup",GenreId=1,PageCount=200,PublishDate = new DateTime(2001,01,11)},
                    new Book{Title="Letup",GenreId=2,PageCount=500,PublishDate = new DateTime(2002,02,12)},
                    new Book{Title="ea artup",GenreId=3,PageCount=300,PublishDate = new DateTime(2004,04,14)}
                );
                context.SaveChanges();
            }
        }
    }
}