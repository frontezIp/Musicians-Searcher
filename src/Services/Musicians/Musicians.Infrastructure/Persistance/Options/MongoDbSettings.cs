namespace Musicians.Infrastructure.Persistance.Options
{
    internal class MongoDbSettings
    {
        public string ConnectionString
        {
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
        public string Database { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string User { get; set; } = string.Empty;    
        public string Password { get; set; } = string.Empty;    
    }
}
