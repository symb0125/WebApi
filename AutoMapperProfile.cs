using WebApi.Dtos;

class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddUserDto, User>();
    }
}