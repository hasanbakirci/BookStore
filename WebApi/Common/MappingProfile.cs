using AutoMapper;
using WebApi.Application.BookOperations.GetBooks;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using static WebApi.Application.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.GetBookDetail.GetBookDetailQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
            .ForMember(dest => dest.Genre, opt =>opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book,BooksViewModel>()
            .ForMember(dest => dest.Genre, opt =>opt.MapFrom(src => src.Genre.Name));
            CreateMap<GenreEnum, GenresViewModel>();
            CreateMap<GenreEnum, GenreDetailViewModel>();
        }
    }
}