namespace Chat.DataAccess.Options
{
    public class ChatRolesOptions
    {
        public string Admin { get; set; } = string.Empty;
        public int AdminRolePrecedence { get; set; }
        public string User { get; set; } = string.Empty;
        public int UserRolePrecedence { get; set; }
        public string Creator { get; set; } = string.Empty;
        public int CreatorRolePrecedence { get; set; }
    }
}
