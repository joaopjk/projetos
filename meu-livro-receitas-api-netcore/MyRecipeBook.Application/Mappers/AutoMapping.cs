using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Mappers;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<RequestRegisterUser, User>()
            .ForMember(dest => dest.Password,
                opt => opt.Ignore());
    }
}