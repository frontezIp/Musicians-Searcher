namespace Chat.BusinessLogic.DTOs.ResponseDTOs
{
    public class ChatParticipantResponseDto
    {
        public Guid Id { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public MessengerUserResponseDto MessengerUser { get; set; } = null!;
        public ChatRoleResponseDto ChatRole { get; set; } = null!;
    }
}
