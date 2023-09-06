using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Models;
using Musicians.Domain.Exceptions.BadRequestException;
using Musicians.Application.Interfaces.ServiceHelpers;

namespace Musicians.Application.ServiceHelpers
{
    internal class GenreServiceHelper : IGenreServiceHelper
    {
        private readonly IGenresRepository _genreRepository;

        public GenreServiceHelper(IGenresRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<Genre>> CheckIfGenresByGivenIdsExistsAndGet(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            if (ids is null)
                throw new IdsParameterBadRequestException(nameof(Genre));

            var genres = await _genreRepository.GetByIdsAsync(ids, cancellationToken);

            if (genres.Count() != ids.Count())
                throw new CollectionByIdsBadRequestException(nameof(Genre));

            return genres;
        }

        public async Task CheckIfGinresByGivenIdsExists(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            await CheckIfGenresByGivenIdsExistsAndGet(ids, cancellationToken);
        }
    }
}
