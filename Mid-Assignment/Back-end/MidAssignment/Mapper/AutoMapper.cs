using AutoMapper;
using MidAssignment.DTO;
using MidAssignment.Entities;

namespace MidAssignment.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            //Book
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
            CreateMap<BookWithIdDTO, Book>();
            CreateMap<Book, BookWithIdDTO>();

            //Category
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<CategoryWithIdDTO, Category>();
            CreateMap<Category, CategoryWithIdDTO>();

            //BookRequest
            CreateMap<BookRequest, BookRequestDTO>();
            CreateMap<BookRequestDTO, BookRequest>();
            CreateMap<BookRequestWithIdDTO, BookRequest>();
            CreateMap<BookRequest, BookRequestWithIdDTO>();
            CreateMap<BookRequestChangeStatusDTO, BookRequest>();
            CreateMap<BookRequest, BookRequestChangeStatusDTO>();

            //BookRequestDetail
            CreateMap<BookRequestDetail, BookRequestDetailDTO>();
            CreateMap<BookRequestDetailDTO, BookRequestDetail>();
        }
    }
}