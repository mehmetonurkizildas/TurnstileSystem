namespace Application.RabbitMq
{
    public interface IRabbitMqProcuder
    {
        public void SendQueueMessage<T>(T @message, string @eventName);
    }
}
