using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Application.RabbitMq
{
    public class RabbitMqProcuder : IRabbitMqProcuder
    {
        private readonly IConfiguration _configuration;
        ConnectionFactory _connectionFactory = new ConnectionFactory();
        public RabbitMqProcuder(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionFactory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"],
                VirtualHost = "/"
            };
        }
        public void SendQueueMessage<T>(T @message, string @eventName)
        {
            IConnection connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare($"{@eventName}-queue", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: $"{@eventName}-queue", body: body);
        }
    }

}