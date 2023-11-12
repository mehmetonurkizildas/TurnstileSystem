using Application.Services.Repositories;
using Core.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class MovementReportRepository : EfRepositoryBase<MovementReport, ApplicationDbContext>, IMovementReportRepository
    {
        public MovementReportRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
