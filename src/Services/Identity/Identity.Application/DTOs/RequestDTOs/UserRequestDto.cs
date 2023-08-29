using Shared.Enums;

namespace Identity.Application.DTOs.RequestDTOs
{
    public class UserRequestDto
    {
        public string? Username { get; set; }    
        public string Password { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? SecondName { get; set; }
        public string? Photo { get; set; }
        public Guid CityId { get; set; }
        public int Age { get; set; }
        public string? Biography { get; set; }
        public SexTypes SexTypeId { get; set; }
    }
}
