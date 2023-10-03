namespace Chat.BusinessLogic.Kafka.ConfigOptions
{
    public class KafkaTopicSelectionOption<Tk,Tv>
        where Tv: class
    {
        public string Topic { get; set; } = string.Empty; 
    }
}
