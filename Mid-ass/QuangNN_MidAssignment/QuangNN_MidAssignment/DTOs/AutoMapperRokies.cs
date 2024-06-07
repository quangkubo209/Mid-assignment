
using AutoMapper;
using Infrastructure.Models;
using QuangNN_MidAssignment.DTOs.requestDTO;
using QuangNN_MidAssignment.DTOs.responseDTO;

namespace QuangNN_MidAssignment.DTOs;

public class AutoMapperRokies : Profile
{
    public AutoMapperRokies()
    {
        CreateMap<CategoryRequestDto, Category>();
        //CreateMap<BookBorrowingRequestRequestDto, BookBorrowingRequest>();
        CreateMap<UserRequestDto,  User>();
        //CreateMap<BookBorrowingRequestDetailsRequestDto, BookBorrowingRequestDetails>();
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<User, UserResponseDto>();
        CreateMap<Book, BookResponseDto>();
        CreateMap<BookBorrowingRequest, BookBorrowingRequestResponseDto>();
    }
}
