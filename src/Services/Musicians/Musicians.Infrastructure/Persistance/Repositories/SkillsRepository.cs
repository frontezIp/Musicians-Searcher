using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Models;
using Musicians.Infrastructure.Persistance.Contexts;

namespace Musicians.Infrastructure.Persistance.Repositories
{
    internal class SkillsRepository :
        BaseRepository<Skill>, ISkillsRepository
    {
        public SkillsRepository(MusiciansContext musiciansContext) : base(musiciansContext.GetCollection<Skill>())
        {
        }
    }
}
