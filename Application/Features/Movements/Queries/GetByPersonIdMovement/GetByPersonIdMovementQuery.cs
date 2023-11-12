using Application.Features.Movements.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Movements.Queries.GetByPersonIdMovement
{
    public class GetByPersonIdMovementQuery : IRequest<List<MovementGetByPersonIdDto>>
    {
        public int? PersonId { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }
        public class GetByPersonIdMovementQueryHandler : IRequestHandler<GetByPersonIdMovementQuery, List<MovementGetByPersonIdDto>>
        {
            private readonly IMovementRepository _movementRepository;
            private readonly IMapper _mapper;
            public GetByPersonIdMovementQueryHandler(IMovementRepository movementRepository, IMapper mapper)
            {
                _movementRepository = movementRepository;
                _mapper = mapper;
            }

            public async Task<List<MovementGetByPersonIdDto>> Handle(GetByPersonIdMovementQuery request, CancellationToken cancellationToken)
            {
                var movementQuery = await _movementRepository.Query();
                DateTime dateStart = request.DateStart.UnixTimeStampToDateTime();
                DateTime dateEnd = request.DateEnd.UnixTimeStampToDateTime();
                if (request.PersonId != null && request.PersonId > 0)
                {
                    movementQuery = movementQuery.Where(x => x.PersonId == request.PersonId);
                }
                if (dateStart != DateTime.MinValue && dateStart != DateTime.MaxValue)
                {
                    movementQuery = movementQuery.Where(x => x.MovementTime >= dateStart);
                }
                if (dateEnd != DateTime.MinValue && dateEnd != DateTime.MaxValue)
                {
                    movementQuery = movementQuery.Where(x => x.MovementTime <= dateEnd);
                }

                var movements = movementQuery.Include(x => x.Person).ToList();
                List<MovementGetByPersonIdDto> dtos = _mapper.Map<List<MovementGetByPersonIdDto>>(movements);
                return dtos;
            }
        }
    }
}
