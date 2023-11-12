using Core.Repositories;

namespace Domain.Entities
{
    public class Movement : Entity
    {
        public int PersonId { get; set; }
        public DateTime MovementTime { get; set; } = DateTime.UtcNow;
        public EventType EventType { get; set; }
        public Person Person { get; set; }
    }
}
