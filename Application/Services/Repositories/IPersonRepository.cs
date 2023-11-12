using Core.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IPersonRepository : IAsyncRepository<Person> { }
}
