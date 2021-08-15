using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){
            new Book{Id = 1,Title="Lean Startup",GenreId=1,PageCount=200,PublishDate = new DateTime(2001,01,11)},
            new Book{Id = 2,Title="Letup",GenreId=1,PageCount=500,PublishDate = new DateTime(2002,02,12)},
            new Book{Id = 3,Title="an up",GenreId=1,PageCount=100,PublishDate = new DateTime(2003,03,13)},
            new Book{Id = 4,Title="ea artup",GenreId=1,PageCount=300,PublishDate = new DateTime(2004,04,14)},
        };

        [HttpGet]
        public List<Book> GetBooks(){
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id){
            var book = BookList.Where(b => b.Id == id).SingleOrDefault();
            return book;
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id){
        //     var book = BookList.Where(b => b.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        // Post
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook){
            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);
            if(book is not null)
                return BadRequest();
            BookList.Add(newBook);
            return Ok();
        }

        // Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updatedBook){
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book is null)
                return BadRequest();
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if(book is null)
                return BadRequest();
            BookList.Remove(book);
            return Ok();
        }
    }
}