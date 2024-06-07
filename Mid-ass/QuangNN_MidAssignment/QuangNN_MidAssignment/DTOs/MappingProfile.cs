using AutoMapper;
using Infrastructure.Models;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mapping
            CreateMap<User, UserRequestDto>().ReverseMap();
            CreateMap<User, UserResponseDto>().ReverseMap();

            // Book Mapping
            CreateMap<Book, BookRequestDto>().ReverseMap();
            CreateMap<Book, BookResponseDto>().ReverseMap();

            // Category Mapping
            CreateMap<Category, CategoryRequestDto>().ReverseMap();
            CreateMap<Category, CategoryResponseDto>().ReverseMap();

            // Book Borrowing Request Mapping
            //CreateMap<BookBorrowingRequest, BookBorrowingRequestRequestDto>().ReverseMap();
            CreateMap<BookBorrowingRequest, BookBorrowingRequestResponseDto>().ReverseMap();
            CreateMap<BookBorrowRequestDto, BookBorrowingRequest>()
                .ForMember(dest => dest.BookBorrowingRequestDetails, opt => opt.MapFrom(src => src.BookBorrowingRequestDetails));

            // Book Borrowing Request Details Mapping
            //CreateMap<BookBorrowingRequestDetails, BookBorrowingRequestDetailsRequestDto>().ReverseMap();
            CreateMap<BookBorrowingRequestDetails, BookBorrowingRequestDetailsResponseDto>().ReverseMap();
        }
    }
}
