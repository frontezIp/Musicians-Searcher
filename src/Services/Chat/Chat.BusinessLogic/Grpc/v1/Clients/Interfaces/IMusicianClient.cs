using Musicians.Application.Grpc.v1;

namespace Chat.BusinessLogic.Grpc.v1.Clients.Interfaces
{
    public interface IMusicianClient
    {
        Task<GetMusicianFriendsResponse> GetMessengerUserFriendsAsync(string messengerUserId);
        Task<GetMusicianInformationResponse> GetMessengerUserProfileAsync(string messengerUserId);
    }
}
