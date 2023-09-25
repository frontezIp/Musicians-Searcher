using Chat.DataAccess.RequestParameters;

namespace Chat.BusinessLogic.DTOs.RequestDTOs
{
    public class PaginatedUserChatRoomsRequestDto : RequestParamatersDto
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}
