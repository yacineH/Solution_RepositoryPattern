using AutoMapper;
using Solution_RepositoryPattern.Core.Dtos;
using Solution_RepositoryPattern.Core.Models;

namespace Solution_RepositoryPattern.API.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(dest=>dest.Author_Id,src=>src.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.Nom,src=>src.MapFrom(src=>src.Name))
                .ReverseMap();
            CreateMap<Book, BookDto>()
                .ForMember(dest=>dest.Book_Id,src=>src.MapFrom(src=>src.Id))
                .ForMember(dest=>dest.Book_Title,src=>src.MapFrom(src=>src.Title))
                .ForMember(dest => dest.Author_Id, src => src.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.IsFree, src => src.MapFrom(src => !src.Price.HasValue))
                .ReverseMap();
            CreateMap<Genre, GenreDto>()
                .ReverseMap();
        }
    }
}
