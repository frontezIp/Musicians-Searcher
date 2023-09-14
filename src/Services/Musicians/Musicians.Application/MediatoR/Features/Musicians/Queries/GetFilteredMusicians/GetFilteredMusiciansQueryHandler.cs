﻿using MapsterMapper;
using MediatR;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.RequestFeatures;
using Musicians.Domain.RequestParameters;

namespace Musicians.Application.MediatoR.Features.Musicians.Queries.GetFilteredMusicians
{
    internal class GetFilteredMusiciansQueryHandler
        : IRequestHandler<GetFilteredMusiciansQuery, (IEnumerable<MusicianResponseDto> musicians, MetaData metaData)>
    {
        private readonly IMusiciansRepository _musicianRepository;
        private readonly IMapper _mapper;
        private readonly IGenreServiceHelper _genreServiceHelper;
        private readonly ISkillServiceHelper _skillServiceHelper;

        public GetFilteredMusiciansQueryHandler(IMusiciansRepository musicianRepository,
            IMapper mapper, 
            IGenreServiceHelper genreServiceHelper,
            ISkillServiceHelper skillServiceHelper)
        {
            _musicianRepository = musicianRepository;
            _mapper = mapper;
            _genreServiceHelper = genreServiceHelper;
            _skillServiceHelper = skillServiceHelper;
        }

        public async Task<(IEnumerable<MusicianResponseDto> musicians, MetaData metaData)> Handle(GetFilteredMusiciansQuery request, CancellationToken cancellationToken)
        {
            var musicianParameters = _mapper.Map<MusicianParameters>(request.GetFilteredMusiciansRequestDto);

            await _genreServiceHelper.CheckIfGinresByGivenIdsExists(request.GetFilteredMusiciansRequestDto.GenresIds, cancellationToken);
            await _skillServiceHelper.CheckSkillsByGivenIdsExists(request.GetFilteredMusiciansRequestDto.SkillsIds, cancellationToken);

            var pagedList = await _musicianRepository.GetFilteredMusiciansAsync(musicianParameters, cancellationToken);

            var musiciansToReturn = _mapper.Map<IEnumerable<MusicianResponseDto>>(pagedList);

            return (musicians: musiciansToReturn, metaData: pagedList.MetaData);
        }
    }
}
