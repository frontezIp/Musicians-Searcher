namespace Musicians.Application.DTOs.ResponseDTOs
{
    public class MusicianResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = null!;
        public string Photo { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int Age { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int SubscribersCount { get; set; }
        public int FriendsCount { get; set; }
        public List<GenreResponseDto> FavouriteGenres { get; set; } = new();
        public List<SkillResponseDto> Skills { get; set; } = new();
    }
}
