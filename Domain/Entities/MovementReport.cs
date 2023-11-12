using Core.Repositories;

namespace Domain.Entities
{
    public class MovementReport : Entity
    {
        public int PersonId { get; set; }
        public DateTime FirstEntryTime { get; set; }
        public DateTime? LastEntryTime { get; set; }
        public int Score { get; set; }
        public int Duration { get; set; }
        public Person Person { get; set; }
    }
}
