using System;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

using Xunit;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            // arrange(Hazırlık)
            var book = new Book(){Title = "Test",PageCount=100,PublishDate=new System.DateTime(1990,01,10),GenreId= 1};
            _context.Books.Add(book);
            _context.SaveChanges();
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = new CreateBookCommand.CreateBookModel(){Title = book.Title};
            // act (Çalıstırma)
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidCastException>().And.Message.Should().Be("Kitap zaten mevcut");
            // assert () Doğrulama
        }
    }
}