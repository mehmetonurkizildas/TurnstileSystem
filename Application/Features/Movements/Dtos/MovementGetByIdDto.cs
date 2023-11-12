using Application.Features.Persons.Dtos;

namespace Application.Features.Movements.Dtos
{
    public class MovementGetByIdDto
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }
        public DateTime MovementTime { get; set; }
        public PersonDto Person { get; set; }

    }
}
