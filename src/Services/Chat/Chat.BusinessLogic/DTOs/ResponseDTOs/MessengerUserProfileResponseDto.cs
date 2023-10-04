namespace Chat.BusinessLogic.DTOs.ResponseDTOs
{
    public class MessengerUserProfileResponseDto
    {
        public Guid Id { get; set; }    
        public string? Goal { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public int Age { get; set; }    
        public string Location = string.Empty;
        public string? Biography { get; set; }
        public int SubscribersCount { get; set; }
        public int FriendsCount { get; set; }   
    }
}
