using MediatR;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Exceptions.AlreadyExistsException;

namespace Musicians.Application.MediatoR.Features.Friends.Commands.AddFriend
{
    internal class AddFriendToMusicianCommandHandler
        : IRequestHandler<AddFriendToMusicianCommand>
    {
        private readonly ISubcribersRepository _subcribersRepository;
        private readonly IFriendsRepository _friendsRepository;
        private readonly IMusicianServiceHelper _musicianServiceValidator;
        private readonly ITransactionUnit _transactionUnit;

        public AddFriendToMusicianCommandHandler(ISubcribersRepository subcribersRepository,
            IFriendsRepository friendsRepository,
            IMusicianServiceHelper musicianServiceValidator,
            ITransactionUnit transactionUnit)
        {
            _subcribersRepository = subcribersRepository;
            _friendsRepository = friendsRepository;
            _musicianServiceValidator = musicianServiceValidator;
            _transactionUnit = transactionUnit;

        }

        public async Task Handle(AddFriendToMusicianCommand request, CancellationToken cancellationToken)
        {
            var musician = await _musicianServiceValidator.CheckIfMusicianExistsAndGetAsync(request.musicianId, cancellationToken);

            var musicianToAdd = await _musicianServiceValidator.CheckIfMusicianExistsAndGetAsync(request.musicianToAddId, cancellationToken);

            if (await _friendsRepository.IsAlreadyFriendAsync(musician.Id, musicianToAdd.Id))
                throw new MusicianIsAlreadyFriendException(musician.Id, musicianToAdd.Id);

            if (await _subcribersRepository.IsAlreadySubscriberAsync(musicianToAdd.Id, musician.Id))
                throw new MusicianIsAlreadySubscriberException(musicianToAdd.Id, musician.Id);
            
            if (await _subcribersRepository.IsAlreadySubscriberAsync(musician.Id, musicianToAdd.Id))
            {
                await _subcribersRepository.DeleteFromSubscribersAsync(musician.Id, musicianToAdd.Id);

                await _friendsRepository.AddToFriendsAsync(musician.Id, musicianToAdd.Id);

                await _friendsRepository.AddToFriendsAsync(musicianToAdd.Id, musician.Id);
            }

            else
            {
                await _subcribersRepository.AddToSubcribersAsync(musicianToAdd.Id, musician.Id);
            }
        }
    }
}
