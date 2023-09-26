namespace Musicians.Infrastructure.Kafka.ConfigOptions
{
    public class KafkaTopicOptions
    {
        public string IdentityTopic { get; set; } = string.Empty;
        public string MusiciansTopic { get; set; } = string.Empty;
    }
}
