using Shared.Enums;

namespace Musicians.Domain.RequestParameters
{
    public class MusicianParameters : RequestParameters
    {
        public string SearchTerm { get; set; } = string.Empty;

        public SexTypes SexType { get; set; }

        public List<Guid> GenresIds { get; set; } = new();

        public List<Guid> SkillsIds {  get; set; } = new();  

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public uint MinAge { get; set; } = 1;

        public uint MaxAge { get; set; } = 150;
    }
}
