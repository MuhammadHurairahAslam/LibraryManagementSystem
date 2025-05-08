using AutoMapper;
using LibraryManagementSystem.Application.DTOS.Books;
using LibraryManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<GetBookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
            CreateMap<BookListDto, Book>().ReverseMap();
        }
    }
}
