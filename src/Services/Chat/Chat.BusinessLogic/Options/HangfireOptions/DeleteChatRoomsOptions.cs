namespace Chat.BusinessLogic.Options.HangfireOptions
{
    public class DeleteChatRoomsOptions
    {
        public string Schedule { get; set; } = string.Empty;
        public string JobId { get; set; } = string.Empty;
    }
}
