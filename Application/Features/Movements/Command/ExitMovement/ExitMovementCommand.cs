using Application.Features.Movements.Dtos;
using Application.RabbitMq;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Movements.Command.ExitMovement
{
    public class ExitMovementCommand : IRequest<ExitedMovementDto>
    {
        public int PersonId { get; set; }
        public DateTime MovementTime { get; set; } = DateTime.UtcNow;
        public EventType EventType { get; set; } = EventType.Logout;


        public class ExitMovementCommandHandler : IRequestHandler<ExitMovementCommand, ExitedMovementDto>
        {
            private readonly IMovementRepository _movementRepository;
            private readonly IMapper _mapper;
            private readonly IRabbitMqProcuder _rabbitMqProcuder;
            public ExitMovementCommandHandler(
                IMapper mapper,
                IRabbitMqProcuder rabbitMqProcuder,
                IMovementRepository movementRepository)
            {
                _mapper = mapper;
                _rabbitMqProcuder = rabbitMqProcuder;
                _movementRepository = movementRepository;
            }

            public async Task<ExitedMovementDto> Handle(ExitMovementCommand request, CancellationToken cancellationToken)
            {
                var mappedMovement = _mapper.Map<Movement>(request);
                mappedMovement = await _movementRepository.AddAsync(mappedMovement, cancellationToken);
                var dto = _mapper.Map<ExitedMovementDto>(mappedMovement);
                _rabbitMqProcuder.SendQueueMessage(dto, "movement");
                return dto;
            }
        }
    }
}
