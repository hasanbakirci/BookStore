using System;
using WebApi.DBOperations;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context){
            context.Books.AddRange(
                    new Book{Title="Lean Startup",GenreId=1,PageCount=200,PublishDate = new DateTime(2001,01,11)},
                    new Book{Title="Letup",GenreId=2,PageCount=500,PublishDate = new DateTime(2002,02,12)},
                    new Book{Title="ea artup",GenreId=3,PageCount=300,PublishDate = new DateTime(2004,04,14)}
                );
        }
    }
}