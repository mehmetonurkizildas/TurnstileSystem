using Application.Features.MovementReports.Dtos;
using Application.Features.Movements.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MovementReports.Queries.GetMovementReportQuery
{
    public class GetMovementReportQuery : IRequest<List<GetMovementReportDto>>
    {
        public int? PersonId { get; set; }
        public long? DateStart { get; set; }
        public long? DateEnd { get; set; }

        public class GetMovementReportQueryHandler : IRequestHandler<GetMovementReportQuery,List<GetMovementReportDto>>
        {
            private readonly IMovementReportRepository _movementReportRepository;
            private readonly IMapper _mapper;
            public GetMovementReportQueryHandler(IMovementReportRepository movementReportRepository, IMapper mapper)
            {
                _movementReportRepository = movementReportRepository;
                _mapper = mapper;
            }
            public async Task<List<GetMovementReportDto>> Handle(GetMovementReportQuery request,CancellationToken cancellationToken)
            {
                var movementReportQuery = await _movementReportRepository.Query();
                DateTime dateStart = request.DateStart.UnixTimeStampToDateTime();
                DateTime dateEnd = request.DateEnd.UnixTimeStampToDateTime();
                if (request.PersonId != null && request.PersonId > 0)
                {
                    movementReportQuery = movementReportQuery.Where(x => x.PersonId == request.PersonId);
                }
                if (dateStart != DateTime.MinValue && dateStart != DateTime.MaxValue)
                {
                    movementReportQuery = movementReportQuery.Where(x => x.FirstEntryTime >= dateStart);
                }
                if (dateEnd != DateTime.MinValue && dateEnd != DateTime.MaxValue)
                {
                    movementReportQuery = movementReportQuery.Where(x => x.LastEntryTime <= dateEnd);
                }

                var movementReports = movementReportQuery.Include(x => x.Person).ToList();
                List<GetMovementReportDto> dtos = _mapper.Map<List<GetMovementReportDto>>(movementReports);
                return dtos;
            }
        }
    }
}
