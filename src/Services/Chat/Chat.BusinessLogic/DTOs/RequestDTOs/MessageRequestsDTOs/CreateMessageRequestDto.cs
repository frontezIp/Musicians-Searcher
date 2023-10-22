namespace Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs
{
    public class CreateMessageRequestDto
    {
        public string Text { get; set; } = string.Empty!;
        public DateTime? Delay { get; set; }    
    }
}
