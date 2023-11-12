using Core.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IMovementRepository : IAsyncRepository<Movement> { }
}
