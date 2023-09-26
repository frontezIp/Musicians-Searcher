namespace Musicians.Infrastructure.Kafka.ConfigOptions
{
    public class KafkaTopicSelectionOption<Tk,Tv>
        where Tv: class
    {
        public string Topic { get; set; } = string.Empty; 
    }
}
