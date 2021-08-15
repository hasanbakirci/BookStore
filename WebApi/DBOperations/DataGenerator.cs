using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.DBOperations
{
    public static class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider){
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any()){
                    return;
                }
                context.Books.AddRange(
                    new Book{Title="Lean Startup",GenreId=1,PageCount=200,PublishDate = new DateTime(2001,01,11)},
                    new Book{Title="Letup",GenreId=1,PageCount=500,PublishDate = new DateTime(2002,02,12)},
                    new Book{Title="ea artup",GenreId=1,PageCount=300,PublishDate = new DateTime(2004,04,14)}
                );
                context.SaveChanges();
            }
        }
    }
}