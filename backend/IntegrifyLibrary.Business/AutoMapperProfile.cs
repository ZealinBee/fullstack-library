using IntegrifyLibrary.Domain;

using AutoMapper;

namespace IntegrifyLibrary.Business;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<GetUserDto, User>();
        CreateMap<User, GetUserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<User, CreateUserDto>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, UpdateUserDto>();
        CreateMap<BookDto, Book>();
        CreateMap<Book, BookDto>();
        CreateMap<CreateLoanDto, Loan>()
        .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)));

        CreateMap<Loan, CreateLoanDto>()
        .ForMember(dest => dest.LoanDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(DateTime.Now)));
    }
}