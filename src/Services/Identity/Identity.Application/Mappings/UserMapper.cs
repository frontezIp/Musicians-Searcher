using Identity.Application.DTOs.RequestDTOs;
using Identity.Application.DTOs.ResponseDTOs;
using Identity.Domain.Models;
using Mapster;
using Shared.Enums;
using Shared.Messages.IdentityMessages;

namespace Identity.Application.Mappings
{
    public class UserMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserResponseDto>()
                .Map(dest => dest.Username, src => src.UserName)
                .Map(dest => dest.Location, src => src.City.Country.Name + ", " + src.City.Name)
                .Map(dest => dest.SexType, src => Enum.GetName(typeof(SexTypes), src.SexTypeId));

            config.NewConfig<User, UserCreatedMessage>()
                .Map(dest => dest.Location, src => src.City.Country.Name + ", " + src.City.Name)
                .Map(dest => dest.Username, src => src.UserName)
                .Map(dest => dest.FullName, src => src.SecondName + src.Name);

            config.NewConfig<UserRequestDto, User>()
                .Map(dest => dest.UserName, src => src.Username);
        }
    }
}
