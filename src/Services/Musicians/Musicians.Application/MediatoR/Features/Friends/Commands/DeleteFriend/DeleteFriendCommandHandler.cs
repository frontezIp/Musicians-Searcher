using MediatR;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Exceptions.NotFoundException;

namespace Musicians.Application.MediatoR.Features.Friends.Commands.DeleteFriend
{
    internal class DeleteFriendCommandHandler :
        IRequestHandler<DeleteFriendCommand>
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IMusicianServiceHelper _musicianServiceValidator;
        private readonly ITransactionUnit _transactionUnit;

        public DeleteFriendCommandHandler(IFriendsRepository friendsRepository,
            IMusicianServiceHelper musicianServiceValidator,
            ITransactionUnit transactionUnit)
        {
            _friendsRepository = friendsRepository;
            _musicianServiceValidator = musicianServiceValidator;
            _transactionUnit = transactionUnit;
        }

        public async Task Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            await _musicianServiceValidator.CheckIfMusicianExistsAsync(request.musicianToDeleteId, cancellationToken);
            await _musicianServiceValidator.CheckIfMusicianExistsAsync(request.musicianId, cancellationToken);

            if (!await _friendsRepository.IsAlreadyFriendAsync(request.musicianId, request.musicianToDeleteId))
                throw new FriendNotFoundException(request.musicianId, request.musicianToDeleteId);

            await _friendsRepository.DeleteFromFriendsAsync(request.musicianId, request.musicianToDeleteId);
            await _friendsRepository.DeleteFromFriendsAsync(request.musicianToDeleteId, request.musicianId);
        }
    }
}

