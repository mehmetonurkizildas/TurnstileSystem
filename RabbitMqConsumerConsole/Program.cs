using Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using RabbitMqConsumerConsole.RabbitMq;

namespace RabbitMqConsumerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
               .Build();

            var serviceProvider = new ServiceCollection()
                .Configure<RabbitMQConfiguration>(configuration.GetSection("RabbitMQ"))
                .AddSingleton(provider => provider.GetRequiredService<IOptions<RabbitMQConfiguration>>().Value)
                .AddSingleton<RabbitMQConsumerService>()
                .AddApplicationDependencies()
                .AddPersistenceServices(configuration)
                .BuildServiceProvider();

            var rabbitMQConsumerService = serviceProvider.GetRequiredService<RabbitMQConsumerService>();

            rabbitMQConsumerService.Start();
            Console.WriteLine("Consumer started.");
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            rabbitMQConsumerService.Stop();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }
    }
}