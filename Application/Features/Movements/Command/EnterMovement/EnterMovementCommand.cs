using Application.Features.Movements.Dtos;
using Application.RabbitMq;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Movements.Command.EnterMovement
{
    public class EnterMovementCommand : IRequest<EnteredMovementDto>
    {
        public int PersonId { get; set; }
        public DateTime MovementTime { get; set; } = DateTime.UtcNow;
        public EventType EventType { get; set; } = EventType.Login;


        public class EnterMovementCommandHandler : IRequestHandler<EnterMovementCommand, EnteredMovementDto>
        {
            private readonly IMovementRepository _movementRepository;
            private readonly IMapper _mapper;
            private readonly IRabbitMqProcuder _rabbitMqProcuder;
            public EnterMovementCommandHandler(
                IMapper mapper,
                IRabbitMqProcuder rabbitMqProcuder,
                IMovementRepository movementRepository)
            {
                _mapper = mapper;
                _rabbitMqProcuder = rabbitMqProcuder;
                _movementRepository = movementRepository;
            }

            public async Task<EnteredMovementDto> Handle(EnterMovementCommand request, CancellationToken cancellationToken)
            {
                Movement mappedMovement = _mapper.Map<Movement>(request);
                mappedMovement = await _movementRepository.AddAsync(mappedMovement, cancellationToken);              

                var dto = _mapper.Map<EnteredMovementDto>(mappedMovement);
                _rabbitMqProcuder.SendQueueMessage(dto, "movement");
                return dto;
            }
        }
    }
}
