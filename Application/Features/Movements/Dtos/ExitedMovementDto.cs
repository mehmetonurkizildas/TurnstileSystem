﻿namespace Application.Features.Movements.Dtos
{
    public class ExitedMovementDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public EventType EventType { get; set; }
        public DateTime MovementTime { get; set; }

    }
}
