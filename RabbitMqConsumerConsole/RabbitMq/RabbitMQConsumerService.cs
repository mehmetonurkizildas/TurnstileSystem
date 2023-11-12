using Application.Services.Repositories;
using Domain.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace RabbitMqConsumerConsole.RabbitMq
{
    public class RabbitMQConsumerService
    {
        private readonly RabbitMQConfiguration _rabbitMQConfig;
        private readonly IMovementReportRepository _movementReportRepository;
        private readonly IMovementRepository _movementRepository;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMQConsumerService(RabbitMQConfiguration rabbitMQConfig, IMovementReportRepository movementReportRepository, IMovementRepository movementRepository)
        {
            _rabbitMQConfig = rabbitMQConfig;
            _movementReportRepository = movementReportRepository;
            _movementRepository = movementRepository;
        }

        public void Start()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _rabbitMQConfig.HostName,
                UserName = _rabbitMQConfig.UserName,
                Password = _rabbitMQConfig.Password,
            };

            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _rabbitMQConfig.QueueName, durable: false, exclusive: false, autoDelete: true, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
                if (message != null)
                {
                    Movement? movement = message.ToJson<Movement>();
                    if (movement != null)
                    {

                        var entryDate = _movementRepository.GetListAsync(x => x.EventType == EventType.Login && x.PersonId == movement.PersonId && x.MovementTime.Day == DateTime.Now.Day && x.MovementTime.Year == DateTime.Now.Year && x.MovementTime.Month == DateTime.Now.Month).Result?.OrderBy(x=>x.MovementTime).FirstOrDefault()?.MovementTime;
                        var exitDate = _movementRepository.GetListAsync(x => x.EventType == EventType.Logout && x.PersonId == movement.PersonId&& x.MovementTime.Day == DateTime.Now.Day && x.MovementTime.Year == DateTime.Now.Year && x.MovementTime.Month == DateTime.Now.Month).Result?.OrderByDescending(x=>x.MovementTime).FirstOrDefault()?.MovementTime;


                        if (entryDate != null && exitDate != null)
                        {
                            var duration = exitDate - entryDate;
                            int score = CalculateScore(duration);

                            var findMovementReportToday = _movementReportRepository.GetAsync(x => x.PersonId == movement.PersonId && x.FirstEntryTime.Day == DateTime.Now.Day && x.FirstEntryTime.Year == DateTime.Now.Year && x.FirstEntryTime.Month == DateTime.Now.Month,enableTracking:false).GetAwaiter().GetResult();
                            if (findMovementReportToday == null)
                            {
                                _movementReportRepository.AddAsync(new MovementReport
                                {
                                    Duration = duration.Value.Seconds,
                                    PersonId = movement.PersonId,
                                    FirstEntryTime = DateTime.UtcNow,
                                    Score = score,
                                }).GetAwaiter().GetResult();
                            }
                            else
                            {
                                findMovementReportToday.Score = score;
                                findMovementReportToday.FirstEntryTime = entryDate.Value;
                                findMovementReportToday.LastEntryTime = exitDate;
                                findMovementReportToday.Duration = duration.Value.Seconds;
                                _movementReportRepository.UpdateAsync(findMovementReportToday).GetAwaiter().GetResult();
                                

                            }
                        }
                        else
                        {
                            _movementReportRepository.AddAsync(new MovementReport
                            {
                                Duration = 0,
                                PersonId = movement.PersonId,
                                FirstEntryTime = DateTime.UtcNow,
                                Score = 0,
                            }).GetAwaiter().GetResult();
                        }

                    }
                }

            };

            _channel.BasicConsume(queue: _rabbitMQConfig.QueueName, autoAck: true, consumer: consumer);
        }

        public void Stop()
        {
            _channel.Close();
            _connection.Close();
        }

        public int CalculateScore(TimeSpan? totalWorkTime)
        {
            if (totalWorkTime == null)
            {
                return 0;
            }
            const double baseScore = 50;
            const double scorePerHour = 10;

            double additionalScore = totalWorkTime.Value.TotalHours * scorePerHour;

            return (int)(baseScore + additionalScore);
        }

    }
}
