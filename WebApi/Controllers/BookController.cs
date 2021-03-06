using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.Application.BookOperations.DeleteBook;
using WebApi.Application.BookOperations.GetBookDetail;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.GetBookDetail.GetBookDetailQuery;
using static WebApi.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        // readonly uygulama içinde değiştirilemez sadece ctor da değişebilir.
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            // var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            // return bookList;
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            BookDetailViewModel result;

            query.BookId = id;
            GetBookDetailQueryValidator validations = new GetBookDetailQueryValidator();
            validations.ValidateAndThrow(query);
            result = query.Handle();
            return Ok(result);
        }

        // [HttpGet]
        // public Book Get([FromQuery] string id){
        //     var book = BookList.Where(b => b.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        // Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            command.Model = newBook;
            //validasyon
            CreateBookCommandValidator validations = new CreateBookCommandValidator();
            validations.ValidateAndThrow(command);
            //ValidationResult result =validations.Validate(command);
            // if(!result.IsValid)
            // foreach (var item in result.Errors)
            // {
            //     Console.WriteLine("Özellik : "+item.PropertyName +" - Error : "+item.ErrorMessage);
            // }
            // else
            command.Handle();
            return Ok();
        }

        // Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);

            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validations = new UpdateBookCommandValidator();
            validations.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);

            command.BookId = id;
            DeleteBookCommandValidator validations = new DeleteBookCommandValidator();
            validations.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }
    }
}