using Core.Repositories;

namespace Domain.Entities
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
        public ICollection<MovementReport> MovementReports { get; set; } = new List<MovementReport>();
    }
}
