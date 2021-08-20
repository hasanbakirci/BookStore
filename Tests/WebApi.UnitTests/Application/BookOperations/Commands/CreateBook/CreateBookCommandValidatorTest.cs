using System;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.CreateBook;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest :IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("test",0,0)]
        [InlineData("test",0,1)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("tes",0,1)]
       public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount,int genreId)
       {
           CreateBookCommand command = new CreateBookCommand(null,null);
           command.Model = new CreateBookModel()
           {
               Title = title,
               PageCount=pageCount,
               PublishDate = DateTime.Now.Date.AddYears(-1),
               GenreId = genreId
           };
           //act
           CreateBookCommandValidator validator = new CreateBookCommandValidator();
           var result = validator.Validate(command);
           // assert
           result.Errors.Count.Should().BeGreaterThan(0);
       } 
    }
}