namespace Identity.Application.DTOs.ResponseDTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }    
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? SecondName { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Biography { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string SexType { get; set; } = string.Empty;
    }
}
