using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text;

namespace Chat.BusinessLogic.Kafka
{
    internal sealed class KafkaDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (typeof(T) == typeof(Null))
            {
                if (data.Length > 0)
                    throw new ArgumentException("The data that supposed to be null is not.");
                return default;
            }

            if (typeof(T) == typeof(Ignore))
                return default;

            var dataJson = Encoding.UTF8.GetString(data);

            return JsonConvert.DeserializeObject<T>(dataJson);
        }
    }
}
