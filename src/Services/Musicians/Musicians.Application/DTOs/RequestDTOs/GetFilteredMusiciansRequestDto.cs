using Shared.Enums;

namespace Musicians.Application.DTOs.RequestDTOs
{
    public class GetFilteredMusiciansRequestDto : RequestParametersDto
    {
        public string SearchTerm { get; set; } = string.Empty;

        public SexTypes SexType { get; set; }

        public List<Guid> GenresIds { get; set; } = new();

        public List<Guid> SkillsIds { get; set; } = new();

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public int MinAge { get; set; } = 1;

        public int MaxAge { get; set; } = 150;
    }
}
