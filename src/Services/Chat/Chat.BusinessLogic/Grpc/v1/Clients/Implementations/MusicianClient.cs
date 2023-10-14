using Chat.BusinessLogic.Grpc.v1.Clients.Interfaces;
using Grpc.Core;
using Musicians.Application.Grpc.v1;

namespace Chat.BusinessLogic.Grpc.v1.Clients.Implementations
{
    internal class MusicianClient : IMusicianClient
    {
        private readonly Musician.MusicianClient _musicianClient;

        public MusicianClient(Musician.MusicianClient musicianClient)
        {
            _musicianClient = musicianClient;
        }

        public async Task<GetMusicianInformationResponse> GetMessengerUserProfileAsync(string messengerUserId)
        {
            var request = new GetMusicianInformationRequest()
            {
                Id = messengerUserId
            };

            var response = await _musicianClient.GetMusicianProfileAsync(request);

            return response;
        }

        public async Task<GetMusicianFriendsResponse> GetMessengerUserFriendsAsync(string messengerUserId)
        {
            var request = new GetMusicianFriendsRequest()
            {
                Id = messengerUserId
            };

            var response = await _musicianClient.GetMusicianFriendsAsync(request);

            return response;
        }
    }
}
