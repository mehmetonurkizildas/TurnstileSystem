using Domain.Entities;

namespace Application.Features.MovementReports.Dtos
{
    public class GetMovementReportDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int Score { get; set; }
        public int Duration { get; set; }
        public string PersonFullName { get; set; }
    }
}
