using Application.Services.Repositories;
using Core.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class MovementRepository : EfRepositoryBase<Movement, ApplicationDbContext>, IMovementRepository
    {
        public MovementRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
