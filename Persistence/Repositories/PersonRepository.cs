using Application.Services.Repositories;
using Core.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class PersonRepository : EfRepositoryBase<Person, ApplicationDbContext>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
