namespace Musicians.Application.DTOs.RequestDTOs
{
    public class UpdateMusicianProfileRequestDto
    {
        public string Goal { get;set; } = string.Empty;
        public List<Guid> FavouriteGenresIds { get; set; } = new();
        public List<Guid> SkillsIds { get; set; } = new();
    }
}
