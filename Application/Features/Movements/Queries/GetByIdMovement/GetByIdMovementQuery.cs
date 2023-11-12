using Application.Features.Movements.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Movements.Queries.GetByIdMovement
{
    public class GetByIdMovementQuery : IRequest<MovementGetByIdDto>
    {
        public int Id { get; set; }
        public class GetByIdMovementQueryHandler : IRequestHandler<GetByIdMovementQuery, MovementGetByIdDto>
        {
            private readonly IMovementRepository _movementRepository;
            private readonly IMapper _mapper;
            public GetByIdMovementQueryHandler(IMovementRepository movementRepository, IMapper mapper)
            {
                _movementRepository = movementRepository;
                _mapper = mapper;
            }

            public async Task<MovementGetByIdDto> Handle(GetByIdMovementQuery request, CancellationToken cancellationToken)
            {
                Movement? movement = await _movementRepository.GetAsync(x => x.Id == request.Id, include: x => x.Include(a => a.Person),
                    cancellationToken: cancellationToken);
                MovementGetByIdDto dto = _mapper.Map<MovementGetByIdDto>(movement);
                return dto;
            }
        }
    }
}
